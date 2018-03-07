using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cfb.CandidatePortal
{
	/// <summary>
	/// Business object representation of a candidate's threshold status, including historic revisions, for a single statement.
	/// </summary>
	[DataContract(Namespace = "http://caccess.nyccfb.info/schema/data")]
	public class ThresholdStatus : IList<ThresholdRevision>
	{
		/// <summary>
		/// The number of the statement reviewed that determines threshold status.
		/// </summary>
		[DataMember(Name = "Statement")]
		readonly byte _statement;

		/// <summary>
		/// Gets the number of the statement reviewed that determines threshold status.
		/// </summary>
		public byte Statement
		{
			get { return _statement; }
		}

		/// <summary>
		/// Gets the latest, most current threshold status revision for this statement.
		/// </summary>
		public ThresholdRevision Current
		{
			get
			{
				return this.Count > 0 ? this[0] : null;
			}
		}

		/// <summary>
		/// The collection of historic revisions for this threshold status.
		/// </summary>
		[DataMember(Name = "Revisions")]
		private readonly List<ThresholdRevision> _revisions;

		/// <summary>
		/// Gets the collection of historic revisions for this threshold status.
		/// </summary>
		public List<ThresholdRevision> Revisions
		{
			get { return _revisions; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ThresholdStatus"/> class for a specific statement.
		/// </summary>
		/// <param name="statement">The number of the statement reviewed.</param>
		internal ThresholdStatus(byte statement)
		{
			_statement = statement;
			_revisions = new List<ThresholdRevision>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ThresholdStatus"/> class.
		/// </summary>
		private ThresholdStatus()
		{
			_revisions = new List<ThresholdRevision>();
		}

		#region IList<ThresholdHistory> Members

		/// <summary>
		/// Determines the index of a specific item in the IList{ThresholdRevision}.
		/// </summary>
		/// <param name="item">The object to locate in the IList{ThresholdRevision}.</param>
		/// <returns>The index of item if found in the list; otherwise, -1.</returns>
		public int IndexOf(ThresholdRevision item)
		{
			return _revisions.IndexOf(item);
		}

		/// <summary>
		/// Inserts an item to the IList{ThresholdRevision} at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which item should be inserted.</param>
		/// <param name="item">The object to insert into the IList{ThresholdRevision}.</param>
		public void Insert(int index, ThresholdRevision item)
		{
			_revisions.Insert(index, item);
		}

		/// <summary>
		/// Removes the IList{ThresholdRevision} item at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		public void RemoveAt(int index)
		{
			_revisions.RemoveAt(index);
		}

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		public ThresholdRevision this[int index]
		{
			get { return _revisions[index]; }
			set { _revisions[index] = value; }
		}

		#endregion

		#region ICollection<ThresholdRevision> Members

		/// <summary>
		/// Adds an item to the <see cref="ICollection{T}"/>.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
		public void Add(ThresholdRevision item)
		{
			_revisions.Add(item);
		}

		/// <summary>
		/// Removes all items from the <see cref="ICollection{T}"/>.
		/// </summary>
		public void Clear()
		{
			_revisions.Clear();
		}

		/// <summary>
		/// Determines whether the <see cref="ICollection{T}"/> contains a specific value.
		/// </summary>
		/// <param name="item">The object to locate in the <see cref="ICollection{T}"/>.</param>
		/// <returns>true if item is found in the <see cref="ICollection{T}"/>; otherwise, false.</returns>
		public bool Contains(ThresholdRevision item)
		{
			return _revisions.Contains(item);
		}

		/// <summary>
		/// Copies the elements of the <see cref="ICollection{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
		public void CopyTo(ThresholdRevision[] array, int arrayIndex)
		{
			_revisions.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Gets the number of elements contained in the <see cref="ICollection{T}"/>.
		/// </summary>
		public int Count
		{
			get { return _revisions.Count; }
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="ICollection{T}"/> is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get { return ((ICollection<ThresholdRevision>)_revisions).IsReadOnly; }
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"/>.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
		/// <returns>true if item was successfully removed from the <see cref="ICollection{T}"/>; otherwise, false. This method also returns false if item is not found in the original <see cref="ICollection{T}"/>.</returns>
		public bool Remove(ThresholdRevision item)
		{
			return _revisions.Remove(item);
		}

		#endregion

		#region IEnumerable<ThresholdRevision> Members

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.</returns>
		public IEnumerator<ThresholdRevision> GetEnumerator()
		{
			return _revisions.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _revisions.GetEnumerator();
		}

		#endregion
	}
}
