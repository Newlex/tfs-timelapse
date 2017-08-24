using AutoMapper;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using Timelase.Core.Entities;

namespace Timelase.TFS
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TfvcChangesetRef, TfsChangeset>();
            CreateMap<IdentityRef, TfsIdentity>();
        }
    }
}
