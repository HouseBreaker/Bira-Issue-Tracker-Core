using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using BiraIssueTrackerCore.Models;
using Microsoft.AspNetCore.Html;


namespace BiraIssueTrackerCore.Web.Extensions
{
	public static class HtmlExtensions
	{
		public static HtmlString DisplayName(this Enum item)
		{
			var type = item.GetType();
			var members = type.GetMember(item.ToString());
			var displayName = (DisplayAttribute)members
				.First()
				.GetCustomAttributes(typeof(DisplayAttribute), false)
				.FirstOrDefault();

			if (displayName != null)
			{
				return new HtmlString(displayName.Name);
			}

			return new HtmlString(item.ToString());
		}

		public static HtmlString GetColor(this State state)
		{
			string color = null;
			switch (state)
			{
				case State.Open:
					color = "success";
					break;
				case State.InProgress:
					color = "primary";
					break;
				case State.Done:
					color = "warning";
					break;
				case State.Closed:
					color = "danger";
					break;
			}

			return new HtmlString(color);
		}
	}
}
