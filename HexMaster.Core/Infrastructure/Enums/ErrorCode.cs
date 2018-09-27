namespace HexMaster.Keesz.Core.Infrastructure.Enums
{
    public enum ErrorCode
    {

        /* General exceptions */
        UnknownError = 1000,
        GameInvalidPlayerCount = 1001,
        GamePlayerIsNotAFriend = 1002,
        GameNotAPlayerOfThisGame = 1003,

        RegistrationDuplicateEmail = 1030,
        RegistrationDuplicateUsername = 1031,
        RegistrationPasswordTooWeak = 1032,
        RegistrationEmailInvalid = 1033,

        FriendNotFoundException = 1050,
        FriendAlreadyFriend=1051,

        /* Move rules */
        InvalidMoveGeneral = 3000,
        InvalidMovePawnProtectedPawn = 3001,
        InvalidMovePassFinisedPawn = 3002,
        InvalidMoveIllegalHomePass = 3003,
        InvalidMoveSwapNotAllowed = 3004,
        InvalidMoveNotYourTurn = 3005,
        InvalidMoveCardNotOwned = 3006,
        InvalidMoveNotSuitableWithPlayedCard = 3007,
        InvalidMoveNoPawnAvailable = 3008,
        InvalidMoveIllegalSplitMoves = 3009,
        InvalidMoveOvershotFinishPositions = 3010,
        InvalidMoveCannotControlThatPawn = 3011,
        InvalidMovePlayableCardFound = 3012,
    }
}