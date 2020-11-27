using System;

namespace Data.Models.ViewModels
{
     public class ErrorViewModel
     {
          public ErrorViewModel(string errorMessage = "", string[] errors = null)
          {
               this.ErrorMessage = errorMessage;
               this.Errors = errors;
          }

          public ErrorViewModel(Exception exception)
          {
			this.Exception = exception;
		}
          
          public string RequestId { get; set; }
          
          public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

          public string ErrorMessage { get; set; }
          
          public string[] Errors { get; set; }
          
          public Exception Exception { get; set; }
	}
}