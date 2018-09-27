using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HexMaster.Core.DomainModels;
using HexMaster.Core.Infrastructure.Enums;
using HexMaster.Keesz.Connect.DataTransferObjects;

namespace HexMaster.Keesz.Connect.DomainModels
{
    public class User : DomainModelBase<Guid>
    {

        private readonly List<Credential> _credentials;

        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsVerified { get; private set; }
        public string VerificationCode { get; private set; }
        public ReadOnlyCollection<Credential> Credentials => _credentials.AsReadOnly();
        public DateTimeOffset FirstLogin { get;  }
        public DateTimeOffset LastLogin { get; private set; }

        public void SetName(string value)
        {
            SetState(TrackingState.Touched);
            if (!Equals(Name, value))
            {
                Name = value;
                SetState(TrackingState.Modified);
            }
        }
        public void SetEmail(string value)
        {
            SetState(TrackingState.Touched);
            if (!Equals(Email, value))
            {
                Email = value;
                SetState(TrackingState.Modified);
            }
        }
        public void SetLastLogin()
        {
            LastLogin = DateTimeOffset.UtcNow;
            SetState(TrackingState.Modified);
        }
        public void SetVerificationCode(string value)
        {
            if (string.IsNullOrWhiteSpace(VerificationCode))
            {
                VerificationCode = value;
                SetState(TrackingState.Modified);
            }
        }
        public void Verify(string code)
        {
            if (!IsVerified && Equals(VerificationCode, code))
            {
                IsVerified = true;
                IsActive = true;
                SetState(TrackingState.Modified);
            }
        }

        public Credential AddCredential(string subject)
        {
            var cred = new Credential( subject);
            _credentials.Add(cred);
            return cred;
        }
        public void DeleteCredential(Guid id)
        {
            var match = Credentials.FirstOrDefault(cred => cred.Id == id);
            match?.Delete();
        }
        public void DeleteCredential(Credential credential)
        {
            var match = Credentials.FirstOrDefault(cred => cred.Id == credential.Id);
            match?.Delete();
        }

        public User() : base(Guid.NewGuid(), TrackingState.Added)
        {
            _credentials = new List<Credential>();
            FirstLogin = DateTimeOffset.UtcNow;
        }

        public User(Guid id, 
            string name,
            string email,
            bool active,
            bool verified,
            string verificationCode,
            List<Credential> credentials,
            DateTimeOffset firstLogin, 
            DateTimeOffset lastLogin)
            : base(id)
        {
            Name = name;
            Email = email;
            _credentials = credentials ?? new List<Credential>();
            FirstLogin = firstLogin;
            LastLogin = lastLogin;
        }

    }
}
