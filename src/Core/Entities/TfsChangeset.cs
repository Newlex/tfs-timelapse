using System;

namespace Timelase.Core.Entities
{
	public class TfsChangeset
	{
		public TfsIdentity Author { get; set; }

		public int ChangesetId { get; set; }

		public TfsIdentity CheckedInBy { get; set; }

		public string Comment { get; set; }

		public bool CommentTruncated { get; set; }

		public DateTime CreatedDate { get; set; }

		public string Url { get; set; }
	}
}
