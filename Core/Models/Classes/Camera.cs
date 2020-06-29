using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Camera_Shop.Extension;

namespace Camera_Shop.Models.Classes
{
	[Table("Cameras")]
	public class Camera
	{
		private int _id;
		private string _model;
		private decimal _megapixels;
		private int _baseISO;
		private int _maxISO;
		private Brand _brand;

		[Key]
		[SkipProperty]
		public int Id
		{
			get => this._id;
			set => this._id = value;
		}

		[Required]
		public string BrandId { get; set; }

		[NotNull]
		public Brand Brand
		{
			get => this._brand;
			set
			{
				if(value == null)
				{
					throw new ArgumentException("Brand cannot be null!");
				}
				
				this._brand = value;
			}
		}

		[NotNull]
		[MinLength(3)]
		[Display(Name = "Model")]
		[Required(ErrorMessage = "Model is required")]
		public string Model
		{
			get => this._model;
			set
			{
				if(value == null)
				{
					throw new ArgumentException("Model can't be null!");
				}
				if(value.Length < 3)
				{
					throw new ArgumentException("Model cannot be shorter than 3!");
				}

				this._model = value;
			}
		}

		[AllowNull]
		[Display(Name = "Megapixels")]
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
		[Display(Name = "Base ISO")]
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
		[Display(Name = "Max ISO")]
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