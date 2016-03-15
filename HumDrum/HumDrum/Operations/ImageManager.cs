using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

using HumDrum.Operations;
using HumDrum.Operations.Files;

namespace HumDrum.Operations
{
	/// <summary>
	/// Creates an image manager, which will
	/// scan directories for image files and
	/// perform operations on them.
	/// </summary>
	public class ImageManager
	{
		/// <summary>
		/// A list of all the images in the directory this ImageManager
		/// watches.
		/// </summary>
		public List<Image> Images;

		/// <summary>
		/// The image average data. Calculated with
		/// ProcessPrimaryData.
		/// </summary>
		public List<Tuple<Image, Color>> PrimaryData;

		/// <summary>
		/// Makes a new ImageManager by populating the list with
		/// a list of all the image files.
		/// </summary>
		/// <param name="option"> </param>
		/// <param name="directory">Directory.</param>
		public ImageManager (string directory, SearchOption option)
		{
			Images = new List<Image> ();

			/*
			 * The chunk below loads all of the compatible image
			 * files into the Images list based on the SearchOption
			 * based on the file extension.
			*/
			new DirectorySearch (directory, option)
				.Refine (x => {
				var ext = Path.GetExtension (x).ToLower ();
				return 
				ext.Equals (".bmp") ||
				ext.Equals (".gif") ||
				ext.Equals (".jpg") ||
				ext.Equals (".png") ||
				ext.Equals (".jpeg") ||
				ext.Equals (".tiff");
				}).Files.ForEach (x => Images.Add (Image.FromFile(x)));
		}

		/// <summary>
		/// Gets all the pixels from the given image
		/// </summary>
		/// <returns>The pixels.</returns>
		/// <param name="bmp">Bmp.</param>
		public static Color[] GetPixels(Bitmap bmp){
			var colors = new List<Color> ();

			for (int x = 0; x < bmp.Width; x++) {
				for (int y = 0; y < bmp.Height; y++) {
					colors.Add (bmp.GetPixel (x, y));
				}
			}

			return colors.ToArray ();
		}

		/// <summary>
		/// Gets a rectangular area from the bitmap
		/// </summary>
		/// <returns>The pixels.</returns>
		/// <param name="bmp">Bmp.</param>
		/// <param name="topLeft">Top left.</param>
		/// <param name="bottomRight">Bottom right.</param>
		public static Color[] GetPixels(Bitmap bmp, Point topLeft, Point bottomRight)
		{
			var colors = new List<Color> ();

			for (int x = topLeft.X; x < bottomRight.X; x++) 
			{
				for (int y = topLeft.Y; y < bottomRight.Y; y++) 
				{
					colors.Add (bmp.GetPixel (x, y));
				}
			}

			return colors.ToArray ();
		}

		/// <summary>
		/// Filters extreme colors out of the image
		/// </summary>
		/// <returns>The extreme.</returns>
		/// <param name="extremity">Extremity.</param>
		public static Color[] FilterExtreme(Color[] colors, int extremity)
		{
			var returnColors = new List<Color> ();

			foreach (Color c in colors) {
				if 
				(
						(c.R > extremity && c.R < 256 - (extremity)) &&
						(c.G > extremity && c.G < (256 - extremity)) &&
						(c.G > extremity && c.G < (256 - extremity)))
					returnColors.Add (c);
			}

			return returnColors.ToArray ();
		}

		/// <summary>
		/// Find the average color of a given image.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="img">Image.</param>
		public static Color AverageColor(Color[] colors){

			// If the picture doesn't contain anything, return the neutral color - gray
			if (colors.Length.Equals (0))
				return Color.Gray;

			var color = new Tuple<int, int, int>(0, 0, 0);

			foreach(Color c in colors){
					color = new Tuple<int, int, int> (
					color.Item1 + c.R,
						color.Item2 + c.G,
						color.Item3 + c.B);
			}
				
			color = new Tuple<int, int, int> (
				color.Item1 / colors.Length,
				color.Item2 / colors.Length,
				color.Item3 / colors.Length);

			return Color.FromArgb (color.Item1, color.Item2, color.Item3);
		}

		/// <summary>
		/// Returns the average color from an image.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="img">The image to find the average color of</param>
		public static Color AverageColor(Image img)
		{
			return AverageColor (GetPixels (new Bitmap(img)));
		}

		/// <summary>
		/// Returns the average color from the image, after the extreme
		/// colors (within 25 hues of white or black) have been removed.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="colors">Colors.</param>
		public static Color PrimaryColor(Color[] colors)
		{
			return AverageColor (FilterExtreme (colors, 25));
		}

		/// <summary>
		/// Gets the primary color from the image.
		/// </summary>
		/// <returns>The color.</returns>
		/// <param name="img">Image.</param>
		public static Color PrimaryColor(Image img)
		{
			return PrimaryColor (GetPixels (new Bitmap (img)));
		}

		/// <summary>
		/// Processes the Primary data for the images.
		/// Depending on the image data being observed, this process
		/// is very computationally intensive.
		/// </summary>
		public void ProcessPrimaryData()
		{
			PrimaryData = new List<Tuple<Image, Color>> ();

			foreach (Image img in Images) {
				Console.WriteLine ("Processing image: " + img.ToString ());

				if (img.Height < 2 || img.Width < 2)
					continue;

				PrimaryData.Add (new Tuple <Image, Color> (img, PrimaryColor (img)));
			}
		}

		/// <summary>
		/// Calculate the difference between two colors.
		/// </summary>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static double Difference(Color a, Color b)
		{
			double totalDifference = 0;

			// R-difference
			totalDifference += Math.Sqrt (((a.R - b.R) ^ 2));

			// G-difference
			totalDifference += Math.Sqrt (((a.G - b.G) ^ 2));

			// B-difference
			totalDifference += Math.Sqrt (((a.B - b.B) ^ 2));

			return totalDifference;
		}

		/// <summary>
		/// Return the image that has an average color closest to this color.
		/// </summary>
		/// <returns>The match.</returns>
		/// <param name="color">Color.</param>
		public Image NearestMatch(Color color)
		{
			if (PrimaryData == null)
				ProcessPrimaryData ();
			
			var differences = new List<Tuple<double, Image>> ();

			// Add the difference between this color and the average color to "differences"
			for (int i = 0; i < PrimaryData.Count; i++) 
				differences.Add (new Tuple<double, Image> (
					ImageManager.Difference (color, PrimaryData [i].Item2), 
					PrimaryData [i].Item1));
					
			// Sort the list based on their difference scores
			differences.Sort((d1,d2) => d1.Item1.CompareTo(d2.Item1));

			differences.RemoveAll (item => item.Item1.Equals (Double.NaN));

			if (differences.Count.Equals (0))
				return PrimaryData [0].Item1;

			return differences [0].Item2;
		}

		/// <summary>
		/// Return the image that has an average color closest to the average of
		/// these colors.
		/// </summary>
		/// <returns>The match.</returns>
		/// <param name="colors">Color.</param>
		public Image NearestMatch(Color[] colors)
		{
			return NearestMatch (PrimaryColor (colors));
		}

		/// <summary>
		/// Get the image that's the nearest match for this region in the bitmap
		/// </summary>
		/// <returns>The match.</returns>
		/// <param name="bmp">Bmp.</param>
		/// <param name="topLeft">Top left.</param>
		/// <param name="bottomRight">Bottom right.</param>
		public Image NearestMatch(Bitmap bmp, Point topLeft, Point bottomRight)
		{
			return NearestMatch (GetPixels (bmp, topLeft, bottomRight));
		}
	}
}