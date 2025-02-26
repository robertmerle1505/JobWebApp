using JobWebApp.Model;

namespace JobWebApp.Validation;

public static class CompanyValidator
{
    public static ValidateResult Validate(CompanyModel companyModel)
    {
        var result = new ValidateResult() { IsValid = true };
        if (companyModel == null)
        {
            result.IsValid = false;
            result.ErrorMessages.Add($"{nameof(companyModel)} cannot be null");
        }
        else
        {
            if (string.IsNullOrEmpty(companyModel.Name))
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{nameof(companyModel.Name)} cannot be empty");
            }

            if (string.IsNullOrEmpty(companyModel.Address))
            {
                result.IsValid = false;
                result.ErrorMessages.Add($"{nameof(companyModel.Address)} cannot be empty");
            }
        }

        return result;
    }
}
