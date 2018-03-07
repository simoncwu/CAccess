using System;
using System.Configuration;

namespace Cfb.CandidatePortal.CPConfiguration
{
	/// <summary>
	/// Specifies a collection of attachment repository definitions for the C-Access Candidate Portal.
	/// </summary>
	public class RepositoryElementCollection : ConfigurationElementCollection
	{
		/// <summary>
		/// Gets the type of the <see cref="RepositoryElementCollection"/>.
		/// </summary>
		public override ConfigurationElementCollectionType CollectionType
		{
			get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
		}

		/// <summary>
		/// Gets or sets the <see cref="RepositoryElement"/> at the specified location.
		/// </summary>
		/// <param name="index">The index location of the <see cref="RepositoryElement"/> to get or set.</param>
		/// <returns>The <see cref="RepositoryElement"/> at the specified index.</returns>
		public RepositoryElement this[int index]
		{
			get
			{
				return this.BaseGet(index) as RepositoryElement;
			}
			set
			{
				if (this.BaseGet(index) != null)
					this.BaseRemoveAt(index);
				this.BaseAdd(index, value);
			}
		}

		/// <summary>
		/// Adds an <see cref="RepositoryElement"/> to an <see cref="RepositoryElementCollection"/> instance when overridden in a derived class.
		/// </summary>
		/// <param name="element">The <see cref="RepositoryElement"/> to add.</param>
		public void Add(RepositoryElement element)
		{
			BaseAdd(element);
		}

		/// <summary>
		/// Removes all configuration element objects from the collection.
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}

		/// <summary>
		/// Creates a new <see cref="RepositoryElement"/>.
		/// </summary>
		/// <returns>A new <see cref="RepositoryElement"/>.</returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new RepositoryElement();
		}

		/// <summary>
		/// Gets the element key for a specified configuration element.
		/// </summary>
		/// <param name="element">The <see cref="ConfigurationElement"/> to which the key should be returned.</param>
		/// <returns>An <see cref="Object"/> that acts as the key for the specified KeyValueConfigurationElement.</returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			RepositoryElement are = element as RepositoryElement;
			if (element == null)
				return null;
			else
				return are.Name;
		}

		/// <summary>
		/// Removes a <see cref="RepositoryElement"/> from the collection.
		/// </summary>
		/// <param name="element">The <see cref="RepositoryElement"/> to remove.</param>
		public void Remove(RepositoryElement element)
		{
			BaseRemove(element.Name);
		}

		/// <summary>
		/// Removes a <see cref="RepositoryElement"/> from the collection.
		/// </summary>
		/// <param name="key">The key of the <see cref="RepositoryElement"/> to remove.</param>
		public void Remove(string key)
		{
			BaseRemove(key);
		}

		/// <summary>
		/// Removes the <see cref="RepositoryElement"/> at the specified location.
		/// </summary>
		/// <param name="index">The index location of the <see cref="RepositoryElement"/> to remove.</param>
		public void RemoveAt(int index)
		{
			BaseRemoveAt(index);
		}
	}
}
