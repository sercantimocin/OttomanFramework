//namespace Ottoman.CrossCutting.Additional
//{
//    using System.Collections.Generic;
//    using System.Data.Entity.Infrastructure;
//    using System.Data.Entity.Validation;
//    using System.Globalization;

//    public class EfErrorConverter
//    {
//        public DbEntityValidationResult MapValidationResult(DbEntityEntry entityEntry, ICollection<DbValidationError> validationErrors)
//        {
//            DbEntityValidationResult convertedValidationResult = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

//            foreach (var validationError in validationErrors)
//            {
//                string errorMessage = this.ConvertError(validationError.ErrorMessage);
//                convertedValidationResult.ValidationErrors.Add(new DbValidationError(validationError.PropertyName, errorMessage));
//            }

//            return convertedValidationResult;
//        }

//        private string ConvertError(string errorMessage)
//        {
//            string convertedMessage;

//            if (CultureInfo.CurrentCulture.Name == "tr-TR")
//            {
//                convertedMessage = "Türkçe";
//            }
//            else
//            {
//                convertedMessage = "İngilizce";
//            }

//            return convertedMessage;
//        }
//    }
//}
