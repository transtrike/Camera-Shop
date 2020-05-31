using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Camera_Shop.Extension;

namespace Camera_Shop.Models
{
	[Table("Cameras")]
	public class Camera
	{
		private int _id;
		private string _brand;
		private string _model;
		private decimal _megapixels;
		private int _baseISO;
		private int _maxISO;

		public Camera() { }
		
		public Camera(int id, string brand, string model, decimal megapixels, int baseIso, int maxIso)
		{
			this.Id = id;
			this.Brand = brand;
			this.Model = model;
			this.Megapixels = megapixels;
			this.BaseISO = baseIso;
			this.MaxISO = maxIso;
		}

		[Key]
		[SkipProperty]
		[Required(ErrorMessage = "Id is required")]
		public int Id
		{
			get => this._id;
			set => this._id = value;
		}

		[NotNull]
		[MinLength(3)]
		[Required(ErrorMessage = "Brand is required")]
		public string Brand
		{
			get => this._brand;
			set
			{
				if(value == null)
					throw new ArgumentException("Brand can't be null!");

				this._brand = value;
			}
		}

		[NotNull]
		[MinLength(3)]
		[Required(ErrorMessage = "Model is required")]
		public string Model
		{
			get => this._model;
			set
			{
				if(value == null)
					throw new ArgumentException("Model can't be null!");

				this._model = value;
			}
		}

		[AllowNull]
		[Range(1, Double.MaxValue, ErrorMessage = "Megapixels cannot be a 0 or negative number")]
		public decimal Megapixels
		{
			get => this._megapixels;
			set
			{
				if(value < 1)
					throw new ArgumentException("Megapixels cannot be less than or equal to 0!");
                    
				this._megapixels = value;
			}

		}

		[AllowNull]
		[Range(32, Double.MaxValue, ErrorMessage = "Base ISO cannot be less than 32")]
		public int BaseISO
		{
			get => this._baseISO;
			set
			{
				if(value < 32)
					throw new ArgumentException("Base ISO cannot be less than or equal to 0!");
                    
				this._baseISO = value;
			}
		}

		[AllowNull]
		[Range(32, Double.MaxValue, ErrorMessage = "Max ISO cannot be less than 32")]
		public int MaxISO
		{
			get => this._maxISO;
			set
			{
				if(value < 32)
					throw new ArgumentException("Max ISO cannot be less than or equal to 0!");
				//if(value < this._baseISO)
				//	throw new ArgumentException("Max ISO cannot be less than base ISO!");
                    
				this._maxISO = value;
			}
		}
	}
}	