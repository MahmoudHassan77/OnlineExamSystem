using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamSystem.Common.Dtos;
public record AuthResponse(string Message, List<string> Errors, 
    bool Success, string? Name, string? Username, string? Token, 
    string? RefreshToken, DateTime? Expiration) : BaseResponse(Message, Errors, Success);
