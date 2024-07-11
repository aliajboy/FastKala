using Microsoft.AspNetCore.Identity;

namespace FastKala.Utilities;

public class PersianIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = "ایمیل وارد شده تکراری است!"
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = "نام کاربری وارد شده توسط کاربر دیگری استفاده شده است!"
        };
    }

    public override IdentityError InvalidEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = "ایمیل وارد شده نامعتبر است!"
        };
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateRoleName),
            Description = "نام نقش وارد شده تکراری است!"
        };
    }

    public override IdentityError InvalidRoleName(string role)
    {
        return new IdentityError
        {
            Code = nameof(InvalidRoleName),
            Description = "نام نقش وارد شده نامعتبر است"
        };
    }

    public override IdentityError InvalidToken()
    {
        return new IdentityError
        {
            Code = nameof(InvalidToken),
            Description = "توکن نامعتبر است!"
        };
    }

    public override IdentityError InvalidUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = "نام کاربری وارد شده نامعتبر است"
        };
    }

    public override IdentityError LoginAlreadyAssociated()
    {
        return new IdentityError
        {
            Code = nameof(LoginAlreadyAssociated),
            Description = "شما توسط راه دیگری وارد سایت شده اید!"
        };
    }

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = "رمز عبور وارد شده یکسان نیست!"
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "رمز عبور باید شامل عدد باشد!"
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "رمز عبور وارد شده باید شامل حداقل یک حرف کوچک باشد!"
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "رمز عبور باید شامل حروف خاص مانند @!#$%^& باشد!"
        };
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = "رمز عبور باید شامل " + uniqueChars + " تعداد حروف خاص باشد!"
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "رمز عبور وارد شده باید شامل حداقل یک حرف بزرگ باشد!"
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = "طول رمز عبور باید حداقل " + length + " کاراکتر باشد!"
        };
    }

    public override IdentityError UserAlreadyHasPassword()
    {
        return new IdentityError
        {
            Code = nameof(UserAlreadyHasPassword),
            Description = "شما دارای رمز عبور می باشید!"
        };
    }

    //public override IdentityError UserAlreadyInRole(string role)
    //{
    //    return new IdentityError
    //    {
    //        Code = nameof(UserAlreadyInRole),
    //        Description = string.Format(LocalizedIdentityErrorMessages.UserAlreadyInRole, role)
    //    };
    //}

    //public override IdentityError UserNotInRole(string role)
    //{
    //    return new IdentityError
    //    {
    //        Code = nameof(UserNotInRole),
    //        Description = string.Format(LocalizedIdentityErrorMessages.UserNotInRole, role)
    //    };
    //}

    //public override IdentityError UserLockoutNotEnabled()
    //{
    //    return new IdentityError
    //    {
    //        Code = nameof(UserLockoutNotEnabled),
    //        Description = LocalizedIdentityErrorMessages.UserLockoutNotEnabled
    //    };
    //}

    //public override IdentityError RecoveryCodeRedemptionFailed()
    //{
    //    return new IdentityError
    //    {
    //        Code = nameof(RecoveryCodeRedemptionFailed),
    //        Description = LocalizedIdentityErrorMessages.RecoveryCodeRedemptionFailed
    //    };
    //}

    //public override IdentityError ConcurrencyFailure()
    //{
    //    return new IdentityError
    //    {
    //        Code = nameof(ConcurrencyFailure),
    //        Description = LocalizedIdentityErrorMessages.ConcurrencyFailure
    //    };
    //}

    //public override IdentityError DefaultError()
    //{
    //    return new IdentityError
    //    {
    //        Code = nameof(DefaultError),
    //        Description = LocalizedIdentityErrorMessages.DefaultIdentityError
    //    };
    //}
}