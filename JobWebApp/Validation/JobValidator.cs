using JobWebApp.Model;
using Microsoft.IdentityModel.Tokens;

namespace JobWebApp.Validation;

public class JobValidator
{
    public static ValidateResult Validate(JobModel jobModel)
    {
        var result = new ValidateResult() { IsValid = true };
        if (jobModel == null)
        {
            result.IsValid = false;
            result.ErrorMessages.Add($"{nameof(jobModel)} cannot be null");
        }
        else
        {
            if (jobModel.Title.IsNullOrEmpty())
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{nameof(jobModel.Title)} cannot be empty");
            }

            if (jobModel.Description.IsNullOrEmpty())
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{nameof(jobModel.Description)} cannot be empty");
            }

            if (jobModel.CompanyId <= 0)
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{nameof(jobModel.CompanyId)} is not a company");
            }

        }

        return result;
    }
}
