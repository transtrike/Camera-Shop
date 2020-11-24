namespace Data.Models.ViewModels
{
     public class ErrorViewModel
     {
          public ErrorViewModel(string errorMessage = "", string[] errors = null)
          {
               ErrorMessage = errorMessage;
               Errors = errors;
          }
          
          public string RequestId { get; set; }

          public string ErrorMessage { get; set; }
          
          public string[] Errors { get; set; }
          
          public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
     }
}