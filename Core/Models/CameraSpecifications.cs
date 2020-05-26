using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace Camera_Shop.Models
{
     [Table("CameraSpecifications")]
     public class CameraSpecifications
     {
          // Required properties
          private int _id;
          private int _megapixelCountX;
          private int _megapixelCountY;
          private int _megapixels;
          private int _baseISO;
          private int _maxISO;
          private int _fastestShutterSpeed;
          private decimal _continuesFPS;
          private decimal _singleFPS;
          private string _videoQuality;
          private int _videoMaxFPS;
          private string _batteryType;
          private string _sensorSize;
          private string _sensorTechnology;
          private string _mount;
          private decimal _sizeX;
          private decimal _sizeY;
          private decimal _sizeZ;
          private decimal _weight;
          
          // Not Required properties
          private int _extendedISO;
          private TimeSpan _ratedBatteryLife;
          private bool _wifi;
          private decimal _wifiBand;
          private bool _bluetooth;
          private decimal _shutterLag;

          public CameraSpecifications(int id) => this.Id = id;
          
          //For Reflection purposes
          public CameraSpecifications() {}

          [Required]
          [Key]
          public int Id
          {
               get => this._id;
               set => this._id = value;
          }

          [Required]
          public int MegapixelCountX
          {
               get => this._megapixelCountX;
               set
               {
                    if(value <= 100)
                         throw new ArgumentException("MegapixelsX count can't be less then or equal to 100!");
                    
                    this._megapixelCountX = value;
               }
          }
          
          [Required]
          public int MegapixelCountY
          {
               get => this._megapixelCountY;
               set
               {
                    if(value <= 100)
                         throw new ArgumentException("MegapixelsY count can't be less then or equal to 100!");
                    
                    this._megapixelCountY = value;
               }
          }

          public int Megapixels
          {
               get => this._megapixels;
               private set => this._megapixels = this._megapixelCountX / this._megapixelCountY;
          }

          [Required]
          public int BaseISO
          {
               get => this._baseISO;
               set
               {
                    if(value % 2 != 0)
                         throw new ArgumentException("Base ISO must be dividable by 2!");
                    
                    this._baseISO = value;
               }
          }
          
          [Required]
          public int MaxISO
          {
               get => this._maxISO;
               set
               {
                    if(value % 2 != 0)
                         throw new ArgumentException("Max ISO must be dividable by 2!");
                    
                    this._maxISO = value;
               }
          }
          
          public int ExtendedIso
          {
               get => this._extendedISO;
               set
               {
                    if(value % 2 != 0)
                         throw new ArgumentException("Extended ISO must be dividable by 2!");
                    
                    this._extendedISO = value;
               }
          }

          [Required]
          public int FastestShutterSpeed
          {
               get => this._fastestShutterSpeed;
               set
               {
                    if(value <= 1000)
                         throw new ArgumentException("Fastest shutter speed can't be less than 1/1000!");
                    
                    this._fastestShutterSpeed = value;
               }
          }

          [Required]
          public decimal ContinuesFPS
          {
               get => this._continuesFPS;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("Continues FPS can't be less then or equal to 0!");
                    
                    this._continuesFPS = value;
               }
          }
          
          [Required]
          public decimal SingleFPS
          {
               get => this._singleFPS;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("Single FPS can't be less then or equal to 0!");
                    if(value < this._continuesFPS)
                         throw new ArgumentException("Single FPS can't be less then to the continues FPS!");
                    
                    this._singleFPS = value;
               }
          }

          [Required]
          public string VideoQuality
          {
               get => this._videoQuality;
               set => this._videoQuality = value;
          }

          [Required]
          public int VideoMaxFps
          {
               get => this._videoMaxFPS;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("Video's FPS' can't be less then or equal to 0!");
                    
                    this._videoMaxFPS = value;
               }
          }

          [Required]
          public string BatteryType
          {
               get => this._batteryType;
               set => this._batteryType = value;
          }

          [Required]
          public TimeSpan RatedBatteryLife
          {
               get => this._ratedBatteryLife;
               set
               {
                    if(value <= TimeSpan.Zero)
                         throw new ArgumentException("Rated battery life can't be less than or equal to 0!");
                    
                    this._ratedBatteryLife = value;
               }
          }

          public string SensorSize
          {
               get => this._sensorSize;
               set => this._sensorSize = value;
          }

          [Required]
          public string SensorTechnology
          {
               get => this._sensorTechnology;
               set => this._sensorTechnology = value;
          }

          [Required]
          public string Mount
          {
               get => this._mount;
               set => this._mount = value;
          }

          [Required]
          public decimal SizeX
          {
               get => this._sizeX;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("SizeX can't be less than or equal to 0!");
                    
                    this._sizeX = value;
               }
          }
          
          [Required]
          public decimal SizeY
          {
               get => this._sizeY;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("SizeY can't be less than or equal to 0!");
                    
                    this._sizeY = value;
               }
          }
          
          [Required]
          public decimal SizeZ
          {
               get => this._sizeZ;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("SizeZ can't be less than or equal to 0!");
                    
                    this._sizeZ = value;
               }
          }

          [Required]
          public decimal Weight
          {
               get => this._weight;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("Weight can't be less than or equal to 0!");
                    
                    this._weight = value;
               }
          }

          public bool Wifi
          {
               get => this._wifi;
               set => this._wifi = value;
          }

          public decimal WifiBand
          {
               get => this._wifiBand;
               set => this._wifiBand = value;
          }

          public bool Bluetooth
          {
               get => this._bluetooth;
               set => this._bluetooth = value;
          }

          public decimal ShutterLag
          {
               get => this._shutterLag;
               set
               {
                    if(value <= 0)
                         throw new ArgumentException("Shuuter lag can't be less than or equal to 0!");
                    
                    this._shutterLag = value;
               }
          }
     }
}