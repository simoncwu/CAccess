using System;
using System.Configuration.Provider;

namespace Cfb.CandidatePortal.Cmo
{
	/// <summary>
	/// A collection of objects that inherit the <see cref="AttachmentRepository"/> class.
	/// </summary>
	public sealed class AttachmentRepositoryCollection : ProviderCollection
	{
		/// <summary>
		/// Gets the attachemnt repository in the collection referenced by the specified provider name.
		/// </summary>
		/// <param name="name">The name of the attachment repository.</param>
		/// <returns>An object that inherits the <see cref="AttachmentRepository"/> class.</returns>
		public new AttachmentRepository this[string name]
		{
			get { return base[name] as AttachmentRepository; }
		}

		/// <summary>
		/// Creates a new, empty attachment repository collection.
		/// </summary>
		public AttachmentRepositoryCollection()
		{
		}

		/// <summary>
		/// Adds an attachment repository to the collection.
		/// </summary>
		/// <param name="provider">The attachment repository to add to the collection.</param>
		/// <exception cref="ArgumentNullException"><paramref name="provider"/> is a null reference.</exception>
		/// <exception cref="ArgumentException"><paramref name="provider"/> is not of a type that inherits the <see cref="AttachmentRepository"/> class.</exception>
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
				throw new ArgumentNullException("provider");
			if (!(provider is AttachmentRepository))
				throw new ArgumentException(string.Format("Provider must implement the class '{0}'", typeof(AttachmentRepository).ToString()), "provider");
			base.Add(provider);
		}

		/// <summary>
		/// Copies the attachment repository collection to a one-dimensional array.
		/// </summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="AttachmentRepositoryCollection"/>. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		/// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="array"/> is multidimensional, or <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>, or the number of elements in the source array is greater than the available space from <paramref name="index"/> to the end of the destination array.</exception>
		/// <exception cref="InvalidCastException">The type of the source array cannot be cast automatically to the type of the destination array.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception>
		public void CopyTo(AttachmentRepository[] array, int index)
		{
			base.CopyTo(array, index);
		}
	}
}
