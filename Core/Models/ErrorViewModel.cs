namespace Camera_Shop.Models
{
     public class ErrorViewModel
     {
          public ErrorViewModel(string errorMessage = "", string[] errors = null)
          {
               ErrorMessage = errorMessage;
               ErrorMessages = errors;
          }
          
          public string RequestId { get; set; }

          public string ErrorMessage { get; set; }
          
          public string[] ErrorMessages { get; set; }
          
          public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
     }
}