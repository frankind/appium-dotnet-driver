﻿//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
	public abstract class FindsByMobileAttribute : Attribute, IComparable
	{
		protected List<By> byList = new List<By>();

		private void ValidateByDefinition()
		{
			if (byList.Count == 0)
			{
				throw new IllegalLocatorException("The desired locator strategy is not defines");
			}

			if (byList.Count > 1)
			{
				throw new IllegalLocatorException("There should be defined only one locator strategy. Few strategies are being defined " +
				"right now: " + byList.ToString());
			}
		}

		internal By By
		{
			get
			{
				ValidateByDefinition();
				return byList[0];
			}

		}

		/// <summary>
		/// Determines if two <see cref="FindsByAttribute"/> instances are equal.
		/// </summary>
		/// <param name="one">One instance to compare.</param>
		/// <param name="two">The other instance to compare.</param>
		/// <returns><see langword="true"/> if the two instances are equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator ==(FindsByMobileAttribute one, FindsByMobileAttribute two)
		{
			// If both are null, or both are same instance, return true.
			if (object.ReferenceEquals(one, two))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)one == null) || ((object)two == null))
			{
				return false;
			}

			return one.Equals(two);
		}

		/// <summary>
		/// Determines if two <see cref="FindsByAttribute"/> instances are unequal.
		/// </summary>s
		/// <param name="one">One instance to compare.</param>
		/// <param name="two">The other instance to compare.</param>
		/// <returns><see langword="true"/> if the two instances are not equal; otherwise, <see langword="false"/>.</returns>
		public static bool operator !=(FindsByMobileAttribute one, FindsByMobileAttribute two)
		{
			return !(one == two);
		}

		/// <summary>
		/// Determines if one <see cref="FindsByAttribute"/> instance is greater than another.
		/// </summary>
		/// <param name="one">One instance to compare.</param>
		/// <param name="two">The other instance to compare.</param>
		/// <returns><see langword="true"/> if the first instance is greater than the second; otherwise, <see langword="false"/>.</returns>
		public static bool operator >(FindsByMobileAttribute one, FindsByMobileAttribute two)
		{
			if (one == null)
			{
				throw new ArgumentNullException("one", "Object to compare cannot be null");
			}

			return one.CompareTo(two) > 0;
		}

		/// <summary>
		/// Determines if one <see cref="FindsByAttribute"/> instance is less than another.
		/// </summary>
		/// <param name="one">One instance to compare.</param>
		/// <param name="two">The other instance to compare.</param>
		/// <returns><see langword="true"/> if the first instance is less than the second; otherwise, <see langword="false"/>.</returns>
		public static bool operator <(FindsByMobileAttribute one, FindsByMobileAttribute two)
		{
			if (one == null)
			{
				throw new ArgumentNullException("one", "Object to compare cannot be null");
			}

			return one.CompareTo(two) < 0;
		}

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an 
		/// integer that indicates whether the current instance precedes, follows, or occurs 
		/// in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
		/// <list type="table">
		/// <listheader>Value</listheader><listheader>Meaning</listheader>
		/// <item><description>Less than zero</description><description>This instance precedes <paramref name="obj"/> in the sort order.</description></item>
		/// <item><description>Zero</description><description>This instance occurs in the same position in the sort order as <paramref name="obj"/>.</description></item>
		/// <item><description>Greater than zero</description><description>This instance follows <paramref name="obj"/> in the sort order. </description></item>
		/// </list>
		/// </returns>
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", "Object to compare cannot be null");
			}

			FindsByMobileAttribute other = obj as FindsByMobileAttribute;
			if (other == null)
			{
				throw new ArgumentException("Object to compare must be a FindsByAttribute", "obj");
			}

			if (this.Priority != other.Priority)
			{
				return this.Priority - other.Priority;
			}

			return 0;
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object">Object</see> is equal 
		/// to the current <see cref="System.Object">Object</see>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object">Object</see> to compare with the 
		/// current <see cref="System.Object">Object</see>.</param>
		/// <returns><see langword="true"/> if the specified <see cref="System.Object">Object</see>
		/// is equal to the current <see cref="System.Object">Object</see>; otherwise,
		/// <see langword="false"/>.</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			FindsByMobileAttribute other = obj as FindsByMobileAttribute;
			if (other == null)
			{
				return false;
			}

			if (other.Priority != this.Priority)
			{
				return false;
			}

			return other.By.Equals(this.By);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>A hash code for the current <see cref="System.Object">Object</see>.</returns>
		public override int GetHashCode()
		{
			return this.By.GetHashCode();
		}

		/// <summary>
		/// Gets or sets a value indicating where this attribute should be evaluated relative to other instances
		/// of this attribute decorating the same class member.
		/// </summary>
		[DefaultValue(0)]
		public int Priority { get; set; }

		/// <summary>
		/// Sets the target element id
		/// </summary>
		public String ID
		{
			set
			{
				byList.Add(By.Id(value));
			}
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Sets the target element class name
		/// </summary>
		public String ClassName
		{
			set
			{
				byList.Add(By.ClassName(value));
			}
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Sets the target element tag name
		/// </summary>
		public String TagName
		{
			set
			{
				byList.Add(By.TagName(value));
			}
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Sets the target element name
		/// </summary>
		public String Name
		{
			set
			{
				byList.Add(By.Name(value));
			}
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Sets the target element xpath
		/// </summary>
		public String XPath
		{
			set
			{
				byList.Add(By.XPath(value));
			}
			get
			{
				return null;
			}
		}

	}
}
