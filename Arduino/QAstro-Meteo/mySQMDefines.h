// TSL2591
#define TSL2591UPDATETIME           2000L           // Refresh rate for reading Lux level
#define TSL_INTEGRATIONTIME_LOW     0x00            // 100 millis
#define TSL_INTEGRATIONTIME_200MS   0x01            // 200 millis
#define TSL_INTEGRATIONTIME_MED     0x02            // 300 millis
#define TSL_INTEGRATIONTIME_400MS   0x03            // 400 millis
#define TSL_INTEGRATIONTIME_HIGH    0x04            // 500 millis
#define TSL_INTEGRATIONTIME_MAX     0x05            // 600 millis
#define TSL_GAIN_LOW                0x00            // low gain (1x)  - bits 4 and 5
#define TSL_GAIN_MED                0x10            // medium gain (25x) - 16d, 10h
#define TSL_GAIN_HIGH               0x20            // medium gain (428x) - 32d, 20h
#define TSL_GAIN_MAX                0x30            // max gain (9876x) - 48d, 30h
#define KICKSTARTTHRESHHOLD         100             // kickstart threshold for getlux [time in seconds is val * 2s]
#define KICKSTARTSTR                "Kickstart++"
#define KICKSTARTACTIVATEDSTR       "Kickstart activated"
#define LOWSTR                      "LOW"
#define MEDSTR                      "MED"
#define HIGHSTR                     "HIGH"
#define MAXSTR                      "MAX"
#define MS200STR                    "200MS"
#define MS400STR                    "400MS"
#define LUXISZEROSTR                "lux=0 =0.0001"
#define LUXISVERYLOWSTR             "lux<0.00001"
#define GETLUXSTR                   "getlux()"
#define LUMSTR                      "lum="
#define CH1STR                      "ch1_ir="
#define CH0STR                      "ch0_full="
#define AVGLUXSTR                   "avg-l="

// MLX90614
#define MLX90614_I2CADDR            0x5A
#define SKYASTR                     "SKY-A   : "
#define SKYOSTR                     "SKY-O   : "
#define SKYCLEAR                    0
#define SKYCLOUDY                   1
#define SKYCLEARSTR                 "CLEAR"
#define SKYCLOUDYSTR                "CLOUDY"
#define MLX90614UPDATETIME          6000L             // every 6s
