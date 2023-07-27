using Microsoft.AspNetCore.Identity;
using OnlineExamSystem.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamSystem.Common.Helpers;
public static class HelperMethods
{
    public static BaseResponse validateResponse(IdentityResult result, string errorMessage, string successMessage)
    {
        if (result.Succeeded)
        {
            return new BaseResponse(successMessage, null, true);
        }
        return new BaseResponse(errorMessage, result.Errors.Select(a => a.Description).ToList(), false);
    }

}
