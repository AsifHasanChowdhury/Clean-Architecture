using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    // Domain/Exceptions.cs
    public class AppException(string message) : Exception(message);
    public class ValidationException(string message) : AppException(message);
    public class NotFoundException(string message) : AppException(message);
    public class ForbiddenAccessException(string message) : AppException(message);

}
