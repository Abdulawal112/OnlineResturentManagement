using System;

namespace OnlineResturnatManagement.Shared.DTO.RoleViewModel
{
	public class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
