using System.Threading.Tasks;
using Timelase.Core.Entities;

namespace Timelase.Core
{
	public interface ITfsRepository
	{
		Task<TfsChangeset[]> GetItemChangesets(string path);

		Task<string> GetContent(string path, int changesetId);
	}
}
