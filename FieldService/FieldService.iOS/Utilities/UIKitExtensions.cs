//
//  Copyright 2012  Xamarin Inc.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System;
using System.Linq;
using MonoTouch.AddressBook;
using MonoTouch.CoreLocation;
using MonoTouch.Foundation;
using MonoTouch.MapKit;
using MonoTouch.UIKit;
using FieldService.Data;

namespace FieldService.iOS
{
	/// <summary>
	/// Helper class with extension methods
	/// </summary>
	public static class UIKitExtensions
	{
		/// <summary>
		/// Sets a callback for registering text changed notifications on a UITextField
		/// </summary>
		/// <param name='textField'>
		/// Text field to listen events for
		/// </param>
		/// <param name='callback'>
		/// Callback to handle the event
		/// </param>
		public static void SetDidChangeNotification (this UITextField textField, Action<UITextField> callback)
		{
			if (callback == null)
				throw new ArgumentNullException ("callback");

			NSNotificationCenter.DefaultCenter.AddObserver (UITextField.TextFieldTextDidChangeNotification, _ => callback (textField), textField);
		}

		/// <summary>
		/// Returns true if is landscape
		/// </summary>
		public static bool IsLandscape(this UIInterfaceOrientation orientation)
		{
			return orientation == UIInterfaceOrientation.LandscapeLeft || orientation == UIInterfaceOrientation.LandscapeRight;
		}

		/// <summary>
		/// Returns true if is portrait
		/// </summary>
		public static bool IsPortrait(this UIInterfaceOrientation orientation)
		{
			return orientation == UIInterfaceOrientation.Portrait || orientation == UIInterfaceOrientation.PortraitUpsideDown;
		}

		/// <summary>
		/// Creates an MKPlacemark for an assignment
		/// </summary>
		public static MKPlacemark ToPlacemark (this Assignment assignment)
		{
			var address = new PersonAddress ();
			address.Street = assignment.Title + " - " + assignment.Address;
			address.City = assignment.City;
			address.State = assignment.State;
			address.Country = string.Empty;
			address.Dictionary[new NSString("Assignment")] = assignment.WrapObject();
			
			return new MKPlacemark (new CLLocationCoordinate2D (assignment.Latitude, assignment.Longitude), address.Dictionary);
		}

		/// <summary>
		/// Clears all MKPlacemarks for a map view
		/// </summary>
		public static void ClearPlacemarks(this MKMapView mapView)
		{
			mapView.RemoveAnnotations (mapView.Annotations.OfType<MKPlacemark> ().ToArray ());;
		}
	}
}

