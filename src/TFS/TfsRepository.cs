using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Timelase.Core;
using Timelase.Core.Entities;

namespace Timelase.TFS
{
    public class TfsRepository : ITfsRepository, IDisposable
    {
        private readonly TfvcHttpClient _versionControlClient;

        public TfsRepository(TfsSettings tfsSettings)
        {
            if (tfsSettings == null) throw new ArgumentNullException(nameof(tfsSettings));

            _versionControlClient = new TfvcHttpClient(new Uri(tfsSettings.CollectionUri), new VssCredentials());
        }

        public TfsRepository(Uri tfsUri, string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentOutOfRangeException(nameof(userName));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentOutOfRangeException(nameof(password));

            _versionControlClient = new TfvcHttpClient(tfsUri, new VssCredentials(new VssBasicCredential(userName, password)));
        }

        public void Dispose()
        {
            _versionControlClient.Dispose();
        }

        public async Task<TfsChangeset[]> GetItemChangesets(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentOutOfRangeException(nameof(path));

            var criteria = new TfvcChangesetSearchCriteria { ItemPath = path };
            var changesets = await _versionControlClient.GetChangesetsAsync(string.Empty, searchCriteria: criteria);
            return changesets.Select(Mapper.Map<TfsChangeset>).ToArray();
        }

        public async Task<string> GetContent(string path, int changesetId)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentOutOfRangeException(nameof(path));
            if (changesetId < 1) throw new ArgumentOutOfRangeException(nameof(changesetId));

            var descriptor = new TfvcVersionDescriptor(TfvcVersionOption.None, TfvcVersionType.Changeset, changesetId.ToString());
            var stream = await _versionControlClient.GetItemTextAsync(path, versionDescriptor: descriptor);
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
