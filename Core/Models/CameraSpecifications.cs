using System;
using System.ComponentModel.DataAnnotations;

namespace Camera_Shop.Models
{
     public class CameraSpecifications
     {
          // Required
          private int _id;
          private int[] _megapixelCount;
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
          private decimal[] _size;
          private decimal _weight;
          
          // Not Required
          private int _extendedISO;
          private TimeSpan _ratedBattryLife;
          private bool _wifi;
          private decimal _wifiBand;
          private bool _bluetooth;
          private decimal _shutterLag;

          public CameraSpecifications() { }

          [Required]
          public int Id
          {
               get => this._id;
               set => this._id = value;
          }

          [Required]
          public int[] MegapixelCount
          {
               get => this._megapixelCount;
               set
               {
                    if(value[0] <= 100 && value[1] <= 100)
                         throw new ArgumentException("Megapixel count can't be less then or equal to 100!");
                    
                    this._megapixelCount = value;
               }
          }

          public int Megapixels
          {
               get => this._megapixels;
               private set => this._megapixels = this._megapixelCount[0] / this._megapixelCount[1];
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
          public TimeSpan RatedBattryLife
          {
               get => this._ratedBattryLife;
               set
               {
                    if(value <= TimeSpan.Zero)
                         throw new ArgumentException("Rated battery life can't be less than or equal to 0!");
                    
                    this._ratedBattryLife = value;
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
          public decimal[] Size
          {
               get => this._size;
               set
               {
                    if(value[0] <= 0 || value[1] <= 0 || value[2] <= 0)
                         throw new ArgumentException("Size can't be less than or equal to 0!");
                    
                    this._size = value;
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