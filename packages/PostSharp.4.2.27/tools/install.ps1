﻿param($installPath, $toolsPath, $package, $project)


function PathToUri([string] $path)
{
    return new-object Uri('file://' + $path.Replace("%","%25").Replace("#","%23").Replace("$","%24").Replace("+","%2B").Replace(",","%2C").Replace("=","%3D").Replace("@","%40").Replace("~","%7E").Replace("^","%5E"))
}

function UriToPath([System.Uri] $uri)
{
    return [System.Uri]::UnescapeDataString( $uri.ToString() ).Replace([System.IO.Path]::AltDirectorySeparatorChar, [System.IO.Path]::DirectorySeparatorChar)
}

$targetsFile = [System.IO.Path]::Combine($toolsPath, 'PostSharp.targets')

# Need to load MSBuild assembly if it's not loaded yet.
Add-Type -AssemblyName 'Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'

# Grab the loaded MSBuild project for the project
$msbuild = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($project.FullName) | Select-Object -First 1

# Make the path to the targets file relative.
$projectUri = PathToUri $project.FullName
$targetUri = PathToUri $targetsFile

$relativePath = UriToPath $projectUri.MakeRelativeUri($targetUri)

# Remove elements from previous installations or versions.
$itemsToRemove = @()
$itemsToRemove += $msbuild.Xml.Properties | Where-Object {$_.Name.ToLowerInvariant() -eq "dontimportpostsharp" }
# $itemsToRemove += $msbuild.Xml.Properties | Where-Object {$_.Name.ToLowerInvariant().EndsWith("postsharpignoredpackages") } # Don't remove this so that it stays during upgrades.
$itemsToRemove += $msbuild.Xml.Imports | Where-Object {$_.Project.ToLowerInvariant().EndsWith("postsharp.targets") } 
$itemsToRemove += $msbuild.Xml.Targets | Where-Object {$_.Name.ToLowerInvariant() -eq "ensurepostsharpimported" }


if ($itemsToRemove -and $itemsToRemove.length)
{
    foreach ($itemToRemove in $itemsToRemove)
    {
        $itemToRemove.Parent.RemoveChild($itemToRemove) | out-null
    }
}

# Remove references from PostSharp 1.* and 2.*.
$referencesToRemove = @()
$referencesToRemove += $project.Object.References | Where-Object {$_.Identity.ToLowerInvariant().StartsWith("postsharp.public") } 
$referencesToRemove += $project.Object.References | Where-Object {$_.Identity.ToLowerInvariant().StartsWith("postsharp.laos") } 

if ($referencesToRemove -and $referencesToRemove.length)
{
    foreach ($referenceToRemove in $referencesToRemove)
    {
        $referenceToRemove.Remove()
    }
}



# Set property DontImportPostSharp to prevent locally-installed previous versions of PostSharp to interfere.
$msbuild.Xml.AddProperty( "DontImportPostSharp", "True" ) | Out-Null

# Add import to PostSharp.targets
$import = $msbuild.Xml.AddImport($relativePath)
$import.set_Condition( "Exists('$relativePath')" ) | Out-Null
[string]::Format("Added import of '{0}'.", $relativePath )

 # Add a target to fail the build when our targets are not imported
$target = $msbuild.Xml.AddTarget("EnsurePostSharpImported")
$target.BeforeTargets = "BeforeBuild"
$target.Condition = "'`$(PostSharp30Imported)' == ''"

# if the targets don't exist at the time the target runs, package restore didn't run
$errorTask = $target.AddTask("Error")
$errorTask.Condition = "!Exists('$relativePath')"
$errorTask.SetParameter("Text", "This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://www.postsharp.net/links/nuget-restore.");

# if the targets exist at the time the target runs, package restore ran but the build didn't import the targets.
$errorTask = $target.AddTask("Error")
$errorTask.Condition = "Exists('$relativePath')"
$errorTask.SetParameter("Text", "The build restored NuGet packages. Build the project again to include these packages in the build. For more information, see http://www.postsharp.net/links/nuget-restore.");

# For all the assembly references installed by this package - set CopyLocal = true
$pakcageRefs = $package.AssemblyReferences | %{$_.Name}
foreach ($reference in $project.Object.References)
{
    if ($pakcageRefs -contains $reference.Name + ".dll")
    {
        # To persist the CopyLocal value we have to change it from true to false first
        $reference.CopyLocal = $false;
        $reference.CopyLocal = $true;
    }
}

$project.Save()
$project.Object.Refresh()

# SIG # Begin signature block
# MIIdzQYJKoZIhvcNAQcCoIIdvjCCHboCAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUM66m4nh9OcfxIym8ysLbDzX7
# 746gghi9MIID7jCCA1egAwIBAgIQfpPr+3zGTlnqS5p31Ab8OzANBgkqhkiG9w0B
# AQUFADCBizELMAkGA1UEBhMCWkExFTATBgNVBAgTDFdlc3Rlcm4gQ2FwZTEUMBIG
# A1UEBxMLRHVyYmFudmlsbGUxDzANBgNVBAoTBlRoYXd0ZTEdMBsGA1UECxMUVGhh
# d3RlIENlcnRpZmljYXRpb24xHzAdBgNVBAMTFlRoYXd0ZSBUaW1lc3RhbXBpbmcg
# Q0EwHhcNMTIxMjIxMDAwMDAwWhcNMjAxMjMwMjM1OTU5WjBeMQswCQYDVQQGEwJV
# UzEdMBsGA1UEChMUU3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFu
# dGVjIFRpbWUgU3RhbXBpbmcgU2VydmljZXMgQ0EgLSBHMjCCASIwDQYJKoZIhvcN
# AQEBBQADggEPADCCAQoCggEBALGss0lUS5ccEgrYJXmRIlcqb9y4JsRDc2vCvy5Q
# WvsUwnaOQwElQ7Sh4kX06Ld7w3TMIte0lAAC903tv7S3RCRrzV9FO9FEzkMScxeC
# i2m0K8uZHqxyGyZNcR+xMd37UWECU6aq9UksBXhFpS+JzueZ5/6M4lc/PcaS3Er4
# ezPkeQr78HWIQZz/xQNRmarXbJ+TaYdlKYOFwmAUxMjJOxTawIHwHw103pIiq8r3
# +3R8J+b3Sht/p8OeLa6K6qbmqicWfWH3mHERvOJQoUvlXfrlDqcsn6plINPYlujI
# fKVOSET/GeJEB5IL12iEgF1qeGRFzWBGflTBE3zFefHJwXECAwEAAaOB+jCB9zAd
# BgNVHQ4EFgQUX5r1blzMzHSa1N197z/b7EyALt0wMgYIKwYBBQUHAQEEJjAkMCIG
# CCsGAQUFBzABhhZodHRwOi8vb2NzcC50aGF3dGUuY29tMBIGA1UdEwEB/wQIMAYB
# Af8CAQAwPwYDVR0fBDgwNjA0oDKgMIYuaHR0cDovL2NybC50aGF3dGUuY29tL1Ro
# YXd0ZVRpbWVzdGFtcGluZ0NBLmNybDATBgNVHSUEDDAKBggrBgEFBQcDCDAOBgNV
# HQ8BAf8EBAMCAQYwKAYDVR0RBCEwH6QdMBsxGTAXBgNVBAMTEFRpbWVTdGFtcC0y
# MDQ4LTEwDQYJKoZIhvcNAQEFBQADgYEAAwmbj3nvf1kwqu9otfrjCR27T4IGXTdf
# plKfFo3qHJIJRG71betYfDDo+WmNI3MLEm9Hqa45EfgqsZuwGsOO61mWAK3ODE2y
# 0DGmCFwqevzieh1XTKhlGOl5QGIllm7HxzdqgyEIjkHq3dlXPx13SYcqFgZepjhq
# IhKjURmDfrYwggSjMIIDi6ADAgECAhAOz/Q4yP6/NW4E2GqYGxpQMA0GCSqGSIb3
# DQEBBQUAMF4xCzAJBgNVBAYTAlVTMR0wGwYDVQQKExRTeW1hbnRlYyBDb3Jwb3Jh
# dGlvbjEwMC4GA1UEAxMnU3ltYW50ZWMgVGltZSBTdGFtcGluZyBTZXJ2aWNlcyBD
# QSAtIEcyMB4XDTEyMTAxODAwMDAwMFoXDTIwMTIyOTIzNTk1OVowYjELMAkGA1UE
# BhMCVVMxHTAbBgNVBAoTFFN5bWFudGVjIENvcnBvcmF0aW9uMTQwMgYDVQQDEytT
# eW1hbnRlYyBUaW1lIFN0YW1waW5nIFNlcnZpY2VzIFNpZ25lciAtIEc0MIIBIjAN
# BgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAomMLOUS4uyOnREm7Dv+h8GEKU5Ow
# mNutLA9KxW7/hjxTVQ8VzgQ/K/2plpbZvmF5C1vJTIZ25eBDSyKV7sIrQ8Gf2Gi0
# jkBP7oU4uRHFI/JkWPAVMm9OV6GuiKQC1yoezUvh3WPVF4kyW7BemVqonShQDhfu
# ltthO0VRHc8SVguSR/yrrvZmPUescHLnkudfzRC5xINklBm9JYDh6NIipdC6Anqh
# d5NbZcPuF3S8QYYq3AhMjJKMkS2ed0QfaNaodHfbDlsyi1aLM73ZY8hJnTrFxeoz
# C9Lxoxv0i77Zs1eLO94Ep3oisiSuLsdwxb5OgyYI+wu9qU+ZCOEQKHKqzQIDAQAB
# o4IBVzCCAVMwDAYDVR0TAQH/BAIwADAWBgNVHSUBAf8EDDAKBggrBgEFBQcDCDAO
# BgNVHQ8BAf8EBAMCB4AwcwYIKwYBBQUHAQEEZzBlMCoGCCsGAQUFBzABhh5odHRw
# Oi8vdHMtb2NzcC53cy5zeW1hbnRlYy5jb20wNwYIKwYBBQUHMAKGK2h0dHA6Ly90
# cy1haWEud3Muc3ltYW50ZWMuY29tL3Rzcy1jYS1nMi5jZXIwPAYDVR0fBDUwMzAx
# oC+gLYYraHR0cDovL3RzLWNybC53cy5zeW1hbnRlYy5jb20vdHNzLWNhLWcyLmNy
# bDAoBgNVHREEITAfpB0wGzEZMBcGA1UEAxMQVGltZVN0YW1wLTIwNDgtMjAdBgNV
# HQ4EFgQURsZpow5KFB7VTNpSYxc/Xja8DeYwHwYDVR0jBBgwFoAUX5r1blzMzHSa
# 1N197z/b7EyALt0wDQYJKoZIhvcNAQEFBQADggEBAHg7tJEqAEzwj2IwN3ijhCcH
# bxiy3iXcoNSUA6qGTiWfmkADHN3O43nLIWgG2rYytG2/9CwmYzPkSWRtDebDZw73
# BaQ1bHyJFsbpst+y6d0gxnEPzZV03LZc3r03H0N45ni1zSgEIKOq8UvEiCmRDoDR
# EfzdXHZuT14ORUZBbg2w6jiasTraCXEQ/Bx5tIB7rGn0/Zy2DBYr8X9bCT2bW+IW
# yhOBbQAuOA2oKY8s4bL0WqkBrxWcLC9JG9siu8P+eJRRw4axgohd8D20UaF5Mysu
# e7ncIAkTcetqGVvP6KUwVyyJST+5z3/Jvz4iaGNTmr1pdKzFHTx/kuDDvBzYBHUw
# ggTTMIIDu6ADAgECAhAY2tGeJn3ou0ohWM3MaztKMA0GCSqGSIb3DQEBBQUAMIHK
# MQswCQYDVQQGEwJVUzEXMBUGA1UEChMOVmVyaVNpZ24sIEluYy4xHzAdBgNVBAsT
# FlZlcmlTaWduIFRydXN0IE5ldHdvcmsxOjA4BgNVBAsTMShjKSAyMDA2IFZlcmlT
# aWduLCBJbmMuIC0gRm9yIGF1dGhvcml6ZWQgdXNlIG9ubHkxRTBDBgNVBAMTPFZl
# cmlTaWduIENsYXNzIDMgUHVibGljIFByaW1hcnkgQ2VydGlmaWNhdGlvbiBBdXRo
# b3JpdHkgLSBHNTAeFw0wNjExMDgwMDAwMDBaFw0zNjA3MTYyMzU5NTlaMIHKMQsw
# CQYDVQQGEwJVUzEXMBUGA1UEChMOVmVyaVNpZ24sIEluYy4xHzAdBgNVBAsTFlZl
# cmlTaWduIFRydXN0IE5ldHdvcmsxOjA4BgNVBAsTMShjKSAyMDA2IFZlcmlTaWdu
# LCBJbmMuIC0gRm9yIGF1dGhvcml6ZWQgdXNlIG9ubHkxRTBDBgNVBAMTPFZlcmlT
# aWduIENsYXNzIDMgUHVibGljIFByaW1hcnkgQ2VydGlmaWNhdGlvbiBBdXRob3Jp
# dHkgLSBHNTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAK8kCAgpejWe
# YAyq50s7Ttx8vDxFHLsr4P4pAvlXCKNkhRUn9fGtyDGJXSLoKqqmQrOP+LlVt7G3
# S7P+j34HV+zvQ9tmYhVhz2ANpNje+ODDYgg9VBPrScpZVIUm5SuPG5/r9aGRwjNJ
# 2ENjalJL0o/ocFFN0Ylpe8dw9rPcEnTbe11LVtOWvxV3obD0oiXyrxySZxjl9AYE
# 75C55ADk3Tq1Gf8CuvQ87uCL6zeL7PTXrPL28D2v3XWRMxkdHEDLdCQZIZPZFP6s
# KlLHj9UESeSNY0eIPGmDy/5HvSt+T8WVrg6d1NFDwGdz4xQIfuU/n3O4MwrPXT80
# h5aK7lPoJRUCAwEAAaOBsjCBrzAPBgNVHRMBAf8EBTADAQH/MA4GA1UdDwEB/wQE
# AwIBBjBtBggrBgEFBQcBDARhMF+hXaBbMFkwVzBVFglpbWFnZS9naWYwITAfMAcG
# BSsOAwIaBBSP5dMahqyNjmvDz4Bq1EgYLHsZLjAlFiNodHRwOi8vbG9nby52ZXJp
# c2lnbi5jb20vdnNsb2dvLmdpZjAdBgNVHQ4EFgQUf9Nlp8Ld7LvwMAnzQzn6Aq8z
# MTMwDQYJKoZIhvcNAQEFBQADggEBAJMkSjBfYs/YGpgvPercmS29d/aleSI47MSn
# oHgSrWIORXBkxeeXZi2YCX5fr9bMKGXyAaoIGkfe+fl8kloIaSAN2T5tbjwNbtjm
# BpFAGLn4we3f20Gq4JYgyc1kFTiByZTuooQpCxNvjtsM3SUC26SLGUTSQXoFaUpY
# T2DKfoJqCwKqJRc5tdt/54RlKpWKvYbeXoEWgy0QzN79qIIqbSgfDQvE5ecaJhnh
# 9BFvELWV/OdCBTLbzp1RXii2noXTW++lfUVAco63DmsOBvszNUhxuJ0ni8RlXw2G
# dpxEevaVXPZdMggzpFS2GD9oXPJCSoU4VINf0egs8qwR1qjtY2owggU7MIIEI6AD
# AgECAhBtVZzZIav2n7DtBg0fBzVhMA0GCSqGSIb3DQEBBQUAMIG0MQswCQYDVQQG
# EwJVUzEXMBUGA1UEChMOVmVyaVNpZ24sIEluYy4xHzAdBgNVBAsTFlZlcmlTaWdu
# IFRydXN0IE5ldHdvcmsxOzA5BgNVBAsTMlRlcm1zIG9mIHVzZSBhdCBodHRwczov
# L3d3dy52ZXJpc2lnbi5jb20vcnBhIChjKTEwMS4wLAYDVQQDEyVWZXJpU2lnbiBD
# bGFzcyAzIENvZGUgU2lnbmluZyAyMDEwIENBMB4XDTE0MDYxMjAwMDAwMFoXDTE3
# MDgxMDIzNTk1OVowbTELMAkGA1UEBhMCQ1oxDzANBgNVBAgTBlByYWd1ZTEPMA0G
# A1UEBxMGUHJhZ3VlMR0wGwYDVQQKFBRTaGFycENyYWZ0ZXJzIHMuci5vLjEdMBsG
# A1UEAxQUU2hhcnBDcmFmdGVycyBzLnIuby4wggEiMA0GCSqGSIb3DQEBAQUAA4IB
# DwAwggEKAoIBAQC+b1Bbdq+VGj6EJcdDpYvUSiSDkZJZTocyzilpOHyt2zUNY69y
# wv6yRwbF7Ik7wC+MQCULUQdl11/LnAufJcydQsSsKG1qfW2MSozrcB/s11GW5uQW
# bOMkwIiTc+dHIeakuTiYw9un+DmCSqT9+CUNqadsMHSsBGrA2NmyKf307qkxxoFq
# cZhqHaS7EPf+luvU6FZuU8G4l2D+P1WzfhOcGod4gUFlBWdy8cT6bB3WwFgXnKLg
# ajUn+MdPbextgatezBQ/FEr1O0KYI/oymKOqpaqMiqxD76ZRqayPBlkPPZCcBVTl
# gnIgzAzCsjmo1xrJ3hTjrxYE2gJUmh8TXZK5AgMBAAGjggGNMIIBiTAJBgNVHRME
# AjAAMA4GA1UdDwEB/wQEAwIHgDArBgNVHR8EJDAiMCCgHqAchhpodHRwOi8vc2Yu
# c3ltY2IuY29tL3NmLmNybDBmBgNVHSAEXzBdMFsGC2CGSAGG+EUBBxcDMEwwIwYI
# KwYBBQUHAgEWF2h0dHBzOi8vZC5zeW1jYi5jb20vY3BzMCUGCCsGAQUFBwICMBkW
# F2h0dHBzOi8vZC5zeW1jYi5jb20vcnBhMBMGA1UdJQQMMAoGCCsGAQUFBwMDMFcG
# CCsGAQUFBwEBBEswSTAfBggrBgEFBQcwAYYTaHR0cDovL3NmLnN5bWNkLmNvbTAm
# BggrBgEFBQcwAoYaaHR0cDovL3NmLnN5bWNiLmNvbS9zZi5jcnQwHwYDVR0jBBgw
# FoAUz5mp6nsm9EvJjo/X8AUm7+PSp50wHQYDVR0OBBYEFEI5jc+chSpGcqlmayYz
# SF3yHjQ1MBEGCWCGSAGG+EIBAQQEAwIEEDAWBgorBgEEAYI3AgEbBAgwBgEBAAEB
# /zANBgkqhkiG9w0BAQUFAAOCAQEApG4uGyuFmvEbeoSDvt+5U04XH8Nlq9iscLMe
# eijHVJA6gFi3kzqgEPdshu4UDT7+CQunXXqsx8daGnksxiYMtj1OOVMlLWUKTnFq
# K2mbIEbM14RaTg/GIcpJPj3xnIn+sobQiaWISQs30oG7FByla7yzTAGXNM721F0/
# O6owD9dmCV42QvVzzTh9aJDtxyQtFd1ZW8v4IhgCFtLMpOcE4PQvn/tJXP+ExLC6
# 0RiwE/kgN/3hxiopoOZrVB5r/lbCPI93PC2nQ6QYr+EXn2siWTMCJ3o+C0uRV8Pe
# 8yFrdDMlNkevIAHqNxCVayNXKRO7fKmQLcbREcKaZUn9z6Wa5zCCBgowggTyoAMC
# AQICEFIA5aolVvwahu2WydRLM8cwDQYJKoZIhvcNAQEFBQAwgcoxCzAJBgNVBAYT
# AlVTMRcwFQYDVQQKEw5WZXJpU2lnbiwgSW5jLjEfMB0GA1UECxMWVmVyaVNpZ24g
# VHJ1c3QgTmV0d29yazE6MDgGA1UECxMxKGMpIDIwMDYgVmVyaVNpZ24sIEluYy4g
# LSBGb3IgYXV0aG9yaXplZCB1c2Ugb25seTFFMEMGA1UEAxM8VmVyaVNpZ24gQ2xh
# c3MgMyBQdWJsaWMgUHJpbWFyeSBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eSAtIEc1
# MB4XDTEwMDIwODAwMDAwMFoXDTIwMDIwNzIzNTk1OVowgbQxCzAJBgNVBAYTAlVT
# MRcwFQYDVQQKEw5WZXJpU2lnbiwgSW5jLjEfMB0GA1UECxMWVmVyaVNpZ24gVHJ1
# c3QgTmV0d29yazE7MDkGA1UECxMyVGVybXMgb2YgdXNlIGF0IGh0dHBzOi8vd3d3
# LnZlcmlzaWduLmNvbS9ycGEgKGMpMTAxLjAsBgNVBAMTJVZlcmlTaWduIENsYXNz
# IDMgQ29kZSBTaWduaW5nIDIwMTAgQ0EwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAw
# ggEKAoIBAQD1I0tepdeKuzLp1Ff37+THJn6tGZj+qJ19lPY2axDXdYEwfwRof8sr
# dR7NHQiM32mUpzejnHuA4Jnh7jdNX847FO6G1ND1JzW8JQs4p4xjnRejCKWrsPvN
# amKCTNUh2hvZ8eOEO4oqT4VbkAFPyad2EH8nA3y+rn59wd35BbwbSJxp58CkPDxB
# AD7fluXF5JRx1lUBxwAmSkA8taEmqQynbYCOkCV7z78/HOsvlvrlh3fGtVayejtU
# MFMb32I0/x7R9FqTKIXlTBdOflv9pJOZf9/N76R17+8V9kfn+Bly2C40Gqa0p0x+
# vbtPDD1X8TDWpjaO1oB21xkupc1+NC2JAgMBAAGjggH+MIIB+jASBgNVHRMBAf8E
# CDAGAQH/AgEAMHAGA1UdIARpMGcwZQYLYIZIAYb4RQEHFwMwVjAoBggrBgEFBQcC
# ARYcaHR0cHM6Ly93d3cudmVyaXNpZ24uY29tL2NwczAqBggrBgEFBQcCAjAeGhxo
# dHRwczovL3d3dy52ZXJpc2lnbi5jb20vcnBhMA4GA1UdDwEB/wQEAwIBBjBtBggr
# BgEFBQcBDARhMF+hXaBbMFkwVzBVFglpbWFnZS9naWYwITAfMAcGBSsOAwIaBBSP
# 5dMahqyNjmvDz4Bq1EgYLHsZLjAlFiNodHRwOi8vbG9nby52ZXJpc2lnbi5jb20v
# dnNsb2dvLmdpZjA0BgNVHR8ELTArMCmgJ6AlhiNodHRwOi8vY3JsLnZlcmlzaWdu
# LmNvbS9wY2EzLWc1LmNybDA0BggrBgEFBQcBAQQoMCYwJAYIKwYBBQUHMAGGGGh0
# dHA6Ly9vY3NwLnZlcmlzaWduLmNvbTAdBgNVHSUEFjAUBggrBgEFBQcDAgYIKwYB
# BQUHAwMwKAYDVR0RBCEwH6QdMBsxGTAXBgNVBAMTEFZlcmlTaWduTVBLSS0yLTgw
# HQYDVR0OBBYEFM+Zqep7JvRLyY6P1/AFJu/j0qedMB8GA1UdIwQYMBaAFH/TZafC
# 3ey78DAJ80M5+gKvMzEzMA0GCSqGSIb3DQEBBQUAA4IBAQBWIuY0pMRhy0i5Aa1W
# qGQP2YyRxLvMDOWteqAif99HOEotbNF/cRp87HCpsfBP5A8MU/oVXv50mEkkhYEm
# HJEUR7BMY4y7oTTUxkXoDYUmcwPQqYxkbdxxkuZFBWAVWVE5/FgUa/7UpO15awgM
# QXLnNyIGCb4j6T9Emh7pYZ3MsZBc/D3SjaxCPWU21LQ9QCiPmxDPIybMSyDLkB9d
# jEw0yjzY5TfWb6UgvTTrJtmuDefFmvehtCGRM2+G6Fi7JXx0Dlj+dRtjP84xfJuP
# G5aexVN2hFucrZH6rO2Tul3IIVPCglNjrxINUIcRGz1UUpaKLJw9khoImgUux5Ol
# SJHTMYIEejCCBHYCAQEwgckwgbQxCzAJBgNVBAYTAlVTMRcwFQYDVQQKEw5WZXJp
# U2lnbiwgSW5jLjEfMB0GA1UECxMWVmVyaVNpZ24gVHJ1c3QgTmV0d29yazE7MDkG
# A1UECxMyVGVybXMgb2YgdXNlIGF0IGh0dHBzOi8vd3d3LnZlcmlzaWduLmNvbS9y
# cGEgKGMpMTAxLjAsBgNVBAMTJVZlcmlTaWduIENsYXNzIDMgQ29kZSBTaWduaW5n
# IDIwMTAgQ0ECEG1VnNkhq/afsO0GDR8HNWEwCQYFKw4DAhoFAKB4MBgGCisGAQQB
# gjcCAQwxCjAIoAKAAKECgAAwGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQwHAYK
# KwYBBAGCNwIBCzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFAdEh3ZE
# hh44H4/871oMpKUR3H/oMA0GCSqGSIb3DQEBAQUABIIBALdtQ44NIggtpItEiIwM
# RaQYnzb/tj/t78r4ei5KXAPqlr1k393TmO4pr1f57VuwjfU2+K8PjxpaY4s4MZLv
# zDJnwLSREhfrBZp1/6gwg6APJ/4++GWUuP/eU33b1xg2l0vGYCU/Dy4ASDmQ8bli
# SyCAFAZluibui72CZhtJulmGNtq4myI8PhZ20k6DqhVUj3d6MwQ2c/dold+EjoYO
# ky04g9Wy6yemh82b1oBVMTyXoehF9bq/KqG+djr91ERbtsdjIkxcopwVyk8Q9GmZ
# TPmQprETirTLWU3ACCvWL7u+RmvEr2UbtocHCtpOViwsl8d4o279+aCWjpis3msg
# bF6hggILMIICBwYJKoZIhvcNAQkGMYIB+DCCAfQCAQEwcjBeMQswCQYDVQQGEwJV
# UzEdMBsGA1UEChMUU3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFu
# dGVjIFRpbWUgU3RhbXBpbmcgU2VydmljZXMgQ0EgLSBHMgIQDs/0OMj+vzVuBNhq
# mBsaUDAJBgUrDgMCGgUAoF0wGAYJKoZIhvcNAQkDMQsGCSqGSIb3DQEHATAcBgkq
# hkiG9w0BCQUxDxcNMTYwNTI2MTYzMDA2WjAjBgkqhkiG9w0BCQQxFgQU29trKiym
# j2bGCIUhe+ZmOYg8b9swDQYJKoZIhvcNAQEBBQAEggEAU+UKQK41rx/uNGMqjnkg
# 9S262KiY6CO0GzwzLCtLRhbQ4a5TxuMlxD7zwVaMfIAgEsZ6jQCEA2Rvh+0mOUyw
# Zqm4tXB63/nZMLDrc4Uf3P6MUCzdkfMkMNU8rWQRnHAK87dxg7A6+eFdRk/Ykrcw
# tMeK2UB/InhNRWodEB2Iq7uZ0iez3BcQc/aZw2gUWGJy81oZ9aImXsvLIzbDwGpd
# s1gcGV7yTe3cHQOaLRZqkRzz+7r0hBT4pDP6sLKNEcQIO1ARjZVcBe16n9maKPp4
# tGl3CwgfC7nVcH+WrpTOh2MhJi3SjZNAig/Eqv66FtSU6td5XrRgnRvucPtPVPi+
# BQ==
# SIG # End signature block
