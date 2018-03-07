using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using Cfb.CandidatePortal.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Cfb.CandidatePortal.SharePoint.Controls
{
	/// <summary>
	/// Web-related extensions.
	/// </summary>
	public static class SPControlExtensions
	{
		/// <summary>
		/// Adds a collection of <see cref="CPCalendarItem"/> objects to a <see cref="SPCalendarItemCollection"/> collection.
		/// </summary>
		/// <typeparam name="T">The type of item to add.</typeparam>
		/// <param name="calItems">The collection to add to.</param>
		/// <param name="items">The collection of items to add.</param>
		public static void AddItems<T>(this SPCalendarItemCollection calItems, IEnumerable<T> items) where T : CPCalendarItem
		{
			if (items == null)
				return;
			foreach (var item in items)
			{
				calItems.AddItem(item);
			}
		}

		/// <summary>
		/// Adds a <see cref="CPCalendarItem"/> object to a <see cref="SPCalendarItemCollection"/> collection.
		/// </summary>
		/// <typeparam name="T">The type of item to add.</typeparam>
		/// <param name="calItems">The collection to add to.</param>
		/// <param name="item">The item to add.</param>
		public static void AddItem<T>(this SPCalendarItemCollection calItems, T item) where T : CPCalendarItem
		{
			if (item == null)
				return;
			SPCalendarItem calItem = item.ToSPCalendarItem();
			if (calItem != null)
				calItems.Add(calItem);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public static SPCalendarItem ToSPCalendarItem<T>(this T item) where T : CPCalendarItem
		{
			// set start date to end date for statement reviews and doing business reviews
			DateTime startDate;
			switch (item.CalendarItemType)
			{
				case CPCalendarItemType.StatementReview:
				case CPCalendarItemType.DoingBusinessReview:
					startDate = item.EndDate;
					break;
				default:
					startDate = item.StartDate;
					break;
			}
			if (item.StartDate == DateTime.MinValue || item.EndDate == DateTime.MinValue)
				return null; // invalid dates will throw an ArgumentOutOfRangeException later
			SPCalendarItem calItem = new SPCalendarItem()
			{
				StartDate = startDate,
				CalendarType = (int)SPCalendarType.Gregorian,
				Description = item.Description,
				EndDate = item.EndDate,
				hasEndDate = item.HasEndDate,
				IsAllDayEvent = item.IsAllDayEvent,
				IsRecurrence = item.IsRecurrence,
				ItemID = PageUrlManager.GetCalendarItemID(item),
				Title = item.Title
			};
			// set calendar item link destinations
			string url = PageUrlManager.GetCalendarItemDisplayUrl(item);
			if (!string.IsNullOrEmpty(url))
				calItem.DisplayFormUrl = url;
			calItem.BackgroundColorClassName = string.Format("{0} {1}", calItem.BackgroundColorClassName, item.CalendarItemType).Trim();
			return calItem;
		}
	}
}