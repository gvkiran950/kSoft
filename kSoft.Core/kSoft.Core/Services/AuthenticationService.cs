using kSoft.Core.Constants;
using kSoft.Core.Contracts.Repository;
using kSoft.Core.Contracts.Services;
using kSoft.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace kSoft.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IKSoftRepository _genericRepository;
        
        public AuthenticationService(IKSoftRepository genericRepository)
        {           
            _genericRepository = genericRepository;
        }
        public async Task<AuthenticationResponse> Authenticate(string userName, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.AuthenticateEndpoint
            };

            AuthenticationRequest authenticationRequest = new AuthenticationRequest()
            {
                UserName = userName,
                Password = password
            };

            return await _genericRepository.PostAsync<AuthenticationRequest, AuthenticationResponse>(builder.ToString(), authenticationRequest);
        }
    }
}
