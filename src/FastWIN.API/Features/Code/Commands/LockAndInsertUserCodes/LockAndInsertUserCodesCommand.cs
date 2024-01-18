using MediatR;
using System;

public class LockAndInsertUserCodesCommand : IRequest<Unit>
{
    public string Code { get; }
    public string UserId { get; }

    public LockAndInsertUserCodesCommand(string code, string userId)
    {
        Code = code;
        UserId = userId;
    }
}

