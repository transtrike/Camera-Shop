using System;

namespace Data.Models.ViewModels
{
     public class ErrorViewModel
     {
          public ErrorViewModel(string errorMessage = "") => this.ErrorMessage = errorMessage;
          public ErrorViewModel(ArgumentException exception) => this.ArgumentException = exception;
          public ErrorViewModel(Exception exception) => this.Exception = exception;
          
          public string RequestId { get; set; }
          public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
          public string ErrorMessage { get; set; }
          public ArgumentException ArgumentException { get; set; }
          public Exception Exception { get; set; }
	}
}