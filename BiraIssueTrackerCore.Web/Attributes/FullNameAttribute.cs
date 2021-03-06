﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BiraIssueTrackerCore.Web.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class FullNameAttribute : ValidationAttribute
	{
		private const string FullNamePattern = @"^\w+ (\w+ ?)+$";

		public override bool IsValid(object value)
		{
			var result = value as string;

			var nameMatches = Regex.IsMatch(result, FullNamePattern);

			if (!nameMatches)
			{
				return false;
			}

			return true;
		}
	}
}
