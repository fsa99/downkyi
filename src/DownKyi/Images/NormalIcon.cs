using DownKyi.ViewModels.UserSpace;

namespace DownKyi.Images
{
    public class NormalIcon
    {
        private static NormalIcon instance;
        public static NormalIcon Instance()
        {
            if (instance == null)
            {
                instance = new NormalIcon();
            }
            return instance;
        }

        public NormalIcon()
        {
            Play = new VectorImage
            {
                Height = 12,
                Width = 10.75,
                Data = @"M869.03 707.93 l-465.29 281.31 q-86.66 49.32 -178.65 28.65 q-91.99 -20.66 -146.65 -103.31 q-36 -56 -36 -121.33
                         l0 -562.61 q2.67 -97.32 69.33 -162.64 q66.66 -65.32 167.98 -67.99 q66.66 0 123.99 34.66 l465.29 281.31
                         q83.99 53.33 105.99 143.32 q22 89.99 -30 173.98 q-29.33 46.66 -75.99 74.66 Z",
                Fill = "#FF000000"
            };

            Like = new VectorImage
            {
                Height = 12,
                Width = 14,
                Data = @"M291.53 894.6 l0 -642.53 l4.66 0 l179.59 -220.4 q29.15 -29.15 69.97 -31.49 q40.81 -2.33 72.29 24.5
                         q16.33 16.32 25.66 37.31 q9.33 20.99 7 45.48 l-7 144.6 l274.04 0 q25.65 0 48.39 11.08 q22.74 11.08 39.07 32.07
                         q13.99 19.82 17.49 44.31 q3.5 24.49 -2.33 46.64 l-116.62 382.49 q-15.16 54.81 -57.14 88.63 q-41.98 33.81 -100.28 37.31
                         l-454.79 0 ZM225.06 894.6 l-93.29 0 q-34.98 2.33 -65.3 -14 q-30.32 -16.33 -47.81 -47.81 q-17.49 -31.49 -18.66 -64.13
                         l0 -385.99 q0 -36.15 17.49 -65.89 q17.49 -29.73 48.98 -46.05 q16.32 -8.17 32.65 -12.25 q16.33 -4.08 32.65 -6.41 l93.29 0
                         l0 642.53 Z",
                Fill = "#FF000000"
            };

            Favorite = new VectorImage
            {
                Height = 14,
                Width = 14,
                Data = @"M772.12 303.28 q-37.33 -5.83 -66.49 -28 q-29.16 -22.16 -44.32 -55.99 l-83.99 -176.13 q-22.16 -43.16 -65.91 -43.16
                         q-43.74 0 -69.4 43.16 l-79.32 176.13 q-17.49 33.83 -46.07 55.99 q-28.58 22.17 -64.74 28 l-188.97 29.16
                         q-45.49 9.33 -58.9 48.99 q-13.42 39.66 18.09 75.82 l142.31 146.98 q25.66 24.49 35.57 58.32 q9.91 33.83 5.25 69.99
                         l-32.66 204.13 q-5.83 48.99 29.75 73.49 q35.57 24.5 79.9 3.5 l159.8 -87.48 q33.83 -19.83 71.74 -19.83
                         q37.91 0 71.74 19.83 l160.97 87.48 q43.16 21 77.57 -3.5 q34.41 -24.5 32.08 -73.49 l-37.33 -204.13
                         q-4.67 -36.16 5.25 -69.99 q9.91 -33.83 35.57 -58.32 l142.31 -146.98 q31.5 -36.16 18.09 -75.82
                         q-13.42 -39.66 -58.91 -48.99 l-188.97 -29.16 Z",
                Fill = "#FF000000"
            };

            Share = new VectorImage
            {
                Height = 14,
                Width = 16,
                Data = @"M453.23 283.64 l0 -219.44 q1.16 -26.85 18.67 -45.53 q17.51 -18.68 44.36 -18.68 q23.34 0 40.85 15.17 l441.21 375.85
                         q25.68 22.18 25.68 56.61 q0 34.43 -25.68 57.78 l-441.21 375.85 q-19.84 16.34 -46.11 14 q-26.26 -2.34 -42.61 -22.18
                         q-15.17 -18.67 -15.17 -42.02 l0 -201.93 q-156.41 0 -249.79 61.87 q-93.38 61.87 -178.58 183.25 q-4.67 5.84 -14.01 5.25
                         q-9.34 -0.59 -10.51 -19.25 q-5.83 -241.62 87.54 -405.62 q93.38 -164 365.35 -171 Z",
                Fill = "#FF000000"
            };

            CloudDownload = new VectorImage
            {
                Height = 32,
                Width = 48,
                Data = @"M19.4,10c-0.7-3.4-3.7-6-7.4-6C9.1,4,6.6,5.6,5.3,8C2.3,8.4,0,10.9,0,14c0,3.3,2.7,6,6,6h13c2.8,0,5-2.2,5-5
		                 C24,12.4,22,10.2,19.4,10z M19,18H6c-2.2,0-4-1.8-4-4c0-2.1,1.5-3.8,3.6-4l1.1-0.1L7.1,9C8.1,7.1,9.9,6,12,6c2.6,0,4.9,1.9,5.4,4.4
		                 l0.3,1.5l1.5,0.1c1.6,0.1,2.8,1.4,2.8,3C22,16.6,20.6,18,19,18z M13.4,10h-2.9v3H8l4,4l4-4h-2.6V10z",
                Fill = "#FF000000"
            };

            Folder = new VectorImage
            {
                Height = 32,
                Width = 40,
                Data = @"M20 6h-8l-2-2H4c-1.1 0-1.99.9-1.99 2L2 18c0 1.1.9 2 2 2h16c1.1 0 2-.9 2-2V8c0-1.1-.9-2-2-2zm0 12H4V8h16v10z",
                Fill = "#FF000000"
            };

            Downloading = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"M1,11.7c0.6,0,1,0.5,1,1V24H1.4C0.6,24,0,23.4,0,22.6c0,0,0,0,0,0v-9.9C0,12.1,0.5,11.7,1,11.7C1,11.7,1,11.7,1,11.7z
                         M0,21.9h24v0.7c0,0.8-0.6,1.4-1.3,1.4H1.4C0.6,24,0,23.4,0,22.6V21.9z
                         M23,11.7c0.6,0,1,0.5,1,1v9.9c0,0.8-0.6,1.4-1.3,1.4H22V12.7C22,12.1,22.5,11.7,23,11.7C23,11.7,23,11.7,23,11.7z M13,17.4
                         h-2V1c0-0.6,0.4-1,1-1s1,0.5,1,1c0,0,0,0,0,0V17.4z
                         M6.9,12.5c0.4-0.4,1-0.4,1.4,0l4.9,5.1L12.8,18c-0.5,0.5-1.4,0.5-1.9,0l-4-4.1C6.5,13.5,6.5,12.9,6.9,12.5L6.9,12.5z
                         M16.8,12.5c0.4,0.4,0.4,1.1,0,1.5l-4,4.1c-0.5,0.5-1.4,0.5-1.9,0l-0.5-0.5l4.9-5.1C15.8,12.1,16.4,12.1,16.8,12.5L16.8,12.5z",
                Fill = "#FF000000"
            };

            DownloadFinished = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"M12,0C8.6,0.1,5.8,1.3,3.5,3.5S0.1,8.6,0,12c0.1,3.4,1.3,6.2,3.5,8.5S8.6,23.9,12,24c3.4-0.1,6.2-1.3,8.5-3.5
                         s3.4-5.1,3.5-8.5c-0.1-3.4-1.3-6.2-3.5-8.5S15.4,0.1,12,0z M12,22.5c-3-0.1-5.5-1.1-7.4-3.1S1.6,15,1.5,12c0.1-3,1.1-5.5,3.1-7.4
                         S9,1.6,12,1.5c3,0.1,5.5,1.1,7.4,3.1s3,4.4,3.1,7.4c-0.1,3-1.1,5.5-3.1,7.4S15,22.4,12,22.5z
                         M16.5,8.1c0.2-0.2,0.4-0.2,0.6-0.2c0.5,0,0.9,0.4,0.9,0.9c0,0.2-0.1,0.5-0.2,0.6l-6.5,6.5
                         c-0.2,0.2-0.4,0.3-0.6,0.3c-0.2,0-0.5-0.1-0.7-0.3l-3.8-3.7C6.1,12,6,11.7,6,11.5c0-0.2,0.1-0.5,0.3-0.6c0.2-0.2,0.4-0.3,0.6-0.3
                         c0.2,0,0.5,0.1,0.6,0.2l3.1,3.1L16.5,8.1L16.5,8.1z",
                Fill = "#FF000000"
            };

            CoinIcon = new VectorImage
            {
                Height = 20,
                Width = 20,
                Data = @"M473.7 358.8 l0 -54.42 l-129.01 0 q-16.12 0 -27.21 -11.09 q-11.09 -11.09 -11.09 -27.72 q0 -16.63 11.09 -27.21
                         q11.09 -10.58 27.21 -11.59 l334.62 0 q22.17 1.01 32.76 20.16 q10.58 19.15 0 37.8 q-10.59 18.64 -32.76 19.65 l-129.01 0
                         l0 54.42 q93.73 16.13 153.7 83.66 q59.97 67.52 61.98 163.27 l0 23.18 q-1 17.14 -11.59 27.72 q-10.59 10.58 -27.22 10.58
                         q-16.63 0 -27.72 -10.58 q-11.08 -10.58 -11.08 -27.72 l0 -23.18 q-1.01 -62.49 -39.31 -108.35 q-38.3 -45.86 -98.77 -59.96
                         l0 332.6 q0 16.12 -11.09 27.2 q-11.08 11.09 -27.21 11.09 q-16.13 0 -27.22 -11.09 q-11.08 -11.08 -11.08 -27.2 l0 -332.6
                         q-60.47 14.11 -98.77 59.96 q-38.3 45.86 -39.31 108.35 l0 23.18 q0 17.14 -11.09 27.72 q-11.08 10.58 -27.71 10.58
                         q-16.63 0 -27.21 -10.58 q-10.59 -10.58 -11.59 -27.72 l0 -23.18 q2.01 -95.75 61.98 -163.27 q59.97 -67.53 153.7 -83.66
                         ZM512 1024 q-217.7 -6.05 -361.82 -150.17 q-144.13 -144.13 -150.18 -361.83 q6.05 -217.7 150.18 -361.82
                         q144.13 -144.13 361.82 -150.18 q217.7 6.05 361.83 150.18 q144.13 144.13 150.17 361.82 q-6.05 217.7 -150.17 361.83
                         q-144.13 144.13 -361.83 150.17 ZM512 946.39 q184.44 -5.04 306.89 -127.5 q122.46 -122.45 127.5 -306.89
                         q-5.04 -184.44 -127.5 -306.89 q-122.45 -122.46 -306.89 -127.5 q-184.44 5.04 -306.89 127.5 q-122.46 122.45 -127.5 306.89
                         q5.04 184.44 127.5 306.89 q122.45 122.46 306.89 127.5 Z",
                Fill = "#FF000000"
            };

            MoneyIcon = new VectorImage
            {
                Height = 20,
                Width = 20,
                Data = @"M409.2 444.47 l138.08 0 q28.22 -1.01 46.36 -19.15 q18.14 -18.14 18.14 -45.85 q0 -27.72 -18.14 -46.36
                         q-18.14 -18.65 -46.36 -19.66 l-129.01 0 q-4.03 0 -6.55 3.03 q-2.52 3.02 -2.52 6.04 l0 121.95 ZM658.14 469.67
                         q48.38 30.24 68.03 80.63 q19.66 50.39 4.03 105.32 q-15.63 54.93 -58.96 87.18 q-43.34 32.25 -99.78 33.25 l-153.19 0
                         q-36.29 -1 -60.98 -25.19 q-24.69 -24.19 -25.7 -61.48 l0 -366.87 q1.01 -36.28 25.7 -60.47 q24.69 -24.19 60.98 -25.2
                         l129.01 0 q42.33 0 76.6 21.67 q34.26 21.67 52.41 59.47 q18.14 37.79 13.11 78.11 q-5.04 40.31 -31.25 73.57 ZM571.46 522.08
                         l-162.26 0 l0 167.31 q1 9.07 9.07 9.07 l153.19 0 q36.29 -1.01 60.48 -26.21 q24.19 -25.2 24.19 -61.99 q0 -36.79 -24.19 -61.48
                         q-24.19 -24.7 -60.48 -26.71 ZM512 1024 q-217.7 -6.05 -361.82 -150.17 q-144.13 -144.13 -150.18 -361.83
                         q6.05 -217.7 150.18 -361.82 q144.13 -144.13 361.82 -150.18 q217.7 6.05 361.83 150.18 q144.13 144.13 150.17 361.82
                         q-6.05 217.7 -150.17 361.83 q-144.13 144.13 -361.83 150.17 ZM512 946.39 q184.44 -5.04 306.89 -127.5
                         q122.46 -122.45 127.5 -306.89 q-5.04 -184.44 -127.5 -306.89 q-122.45 -122.46 -306.89 -127.5 q-184.44 5.04 -306.89 127.5
                         q-122.46 122.45 -127.5 306.89 q5.04 184.44 127.5 306.89 q122.45 122.46 306.89 127.5 Z",
                Fill = "#FF000000"
            };

            BindingEmail = new VectorImage
            {
                Height = 20,
                Width = 20,
                Data = @"M512 1024 q-218 -5 -362.5 -149.5 q-144.5 -144.5 -149.5 -362.5 q5 -218 149.5 -362.5 q144.5 -144.5 362.5 -149.5
                         q218 5 362.5 149.5 q144.5 144.5 149.5 362.5 q-5 218 -149.5 362.5 q-144.5 144.5 -362.5 149.5 ZM282 278 l214 164
                         q8 6 16 6 q8 0 16 -6 l214 -164 l0 257 l-230 77 l-230 -77 l0 -257 ZM305 232 q7 -2 15 -2 l384 0 q8 0 15 2 q-3 2 -5 4
                         l-202 154 l-202 -154 q-2 -2 -5 -4 ZM794 518 l0 -262 q-1 -34 -27 -55 q-26 -21 -63 -22 l-384 0 q-37 1 -63 22
                         q-26 21 -27 55 l0 262 l-9 -3 q-25 -8 -46 7 q-21 15 -21 41 l0 154 q1 43 29.5 72 q28.5 29 72.5 30 l512 0 q44 -1 72.5 -30
                         q28.5 -29 29.5 -72 l0 -154 q0 -26 -21 -41 q-21 -15 -46 -7 l-9 3 ZM205 563 l307 103 l307 -103 l0 154 q0 21 -15 36
                         q-15 15 -36 15 l-512 0 q-22 -1 -36.5 -15 q-14.5 -14 -14.5 -36 l0 -154 Z",
                Fill = "#FF000000"
            };

            BindingPhone = new VectorImage
            {
                Height = 20,
                Width = 20,
                Data = @"M512 1024 q-218 -5 -362.5 -149.5 q-144.5 -144.5 -149.5 -362.5 q5 -218 149.5 -362.5 q144.5 -144.5 362.5 -149.5
                         q218 5 362.5 149.5 q144.5 144.5 149.5 362.5 q-5 218 -149.5 362.5 q-144.5 144.5 -362.5 149.5 ZM307 614 l0 -307
                         l410 0 l0 461 q0 21 -15 36 q-15 15 -36 15 l-308 0 q-21 0 -35.5 -14.5 q-14.5 -14.5 -15.5 -36.5 l0 -102 l333 0
                         q11 -1 18 -8 q7 -7 7 -18 q0 -11 -7 -18 q-7 -7 -18 -8 l-333 0 ZM307 256 q0 -21 15 -36 q15 -15 36 -15 l308 0
                         q21 0 36 15 q15 15 15 36 l-410 0 ZM358 154 q-43 1 -72 29.5 q-29 28.5 -30 72.5 l0 512 q1 44 30 72.5 q29 28.5 72 29.5
                         l308 0 q43 -1 72 -29.5 q29 -28.5 30 -72.5 l0 -512 q-1 -44 -30 -72.5 q-29 -28.5 -72 -29.5 l-308 0 ZM512 794
                         q22 -1 36.5 -15.5 q14.5 -14.5 14.5 -36 q0 -21.5 -14.5 -36 q-14.5 -14.5 -36.5 -14.5 q-22 0 -36.5 14.5 q-14.5 14.5 -14.5 36
                         q0 21.5 14.5 36 q14.5 14.5 36.5 15.5 Z",
                Fill = "#FF000000"
            };

            FavoriteOutline = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M755.8 1021.89 q-13.98 -1 -28.45 -4.5 q-14.47 -3.5 -29.45 -8.49 l-146.74 -76.87 q-18.97 -13.97 -39.93 -13.97
                         q-20.97 0 -35.94 13.97 l-146.75 76.87 q-33.94 14.97 -69.38 12.98 q-35.44 -2 -65.39 -25.96 q-23.96 -19.96 -35.44 -49.41
                         q-11.48 -29.45 -2.5 -59.4 l31.95 -184.68 q3.99 -22.96 -3.49 -40.43 q-7.49 -17.47 -22.47 -36.43 l-126.77 -133.77
                         q-24.96 -19.96 -32.45 -50.41 q-7.49 -30.45 6.49 -64.39 q9.98 -28.95 34.94 -49.91 q24.96 -20.97 54.9 -26.96
                         l171.71 -24.95 q19.96 -1 36.93 -12.98 q16.97 -11.98 26.95 -31.94 l69.88 -159.73 q14.98 -33.94 45.42 -53.91
                         q30.44 -19.97 69.38 -15.97 q34.94 0 60.39 20.96 q25.45 20.96 35.44 55.9 l76.86 152.74 q9.98 18.96 26.96 32.44
                         q16.98 13.48 36.93 18.47 l172.7 25.95 q28.95 4.99 51.91 22.96 q22.96 17.97 36.93 46.92 q9.99 28.95 5 59.89
                         q-5 30.95 -23.96 54.91 l-127.78 133.77 q-14.97 14.97 -22.46 33.94 q-7.49 18.97 -3.49 42.92 l31.94 184.68
                         q4.99 34.94 -8.49 65.39 q-13.48 30.45 -42.43 50.41 q-13.98 8.98 -32.44 13.98 q-18.47 5 -37.44 5 ZM513.22 817.24
                         q23.96 0 46.92 5.99 q22.96 5.99 42.93 19.97 l139.75 75.87 q9.99 4.99 15.48 2.99 q5.49 -2 10.48 -2.49
                         q4.99 -0.5 7.99 -4.99 q3 -4.5 -2 -14.48 l-31.95 -184.68 q-4.99 -47.92 6.49 -87.35 q11.48 -39.43 44.42 -72.37
                         l127.78 -133.77 q3.99 -4.99 2.5 -9.98 q-1.5 -4.99 -2 -9.48 q-0.5 -4.5 -4.49 -5.49 q-3.99 -1 -8.99 -1 l-171.7 -24.96
                         q-43.92 -5.99 -79.86 -32.44 q-35.94 -26.45 -60.89 -70.38 l-69.88 -159.72 q0 -8.99 -4 -10.98 q-4 -2 -8.99 -2
                         q-4.99 0 -9.48 2 q-4.49 1.99 -9.48 10.98 l-69.88 159.72 q-24.96 43.92 -61.39 70.38 q-36.44 26.45 -79.37 32.44
                         l-172.7 24.96 q-4.99 0 -8.48 3.99 q-3.5 3.99 -3.99 8.98 q-0.49 5 0.5 8.99 q1 3.99 5.99 3.99 l127.78 133.77
                         q32.94 33.94 44.42 71.88 q11.48 37.93 6.49 80.85 l-25.95 185.68 q0 4.99 1 9.49 q1 4.5 5.99 9.49 q5 4.99 13.48 4.99
                         q8.49 0 18.47 -4.99 l139.76 -76.87 q10.98 -4.99 31.44 -11.48 q20.47 -6.49 45.42 -7.49 Z",
                Fill = "#FF000000"
            };

            Subscription = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M512 945.07 q-6.15 0 -13.53 -2.46 q-7.39 -2.46 -13.54 -4.92 q-43.07 -32 -190.74 -153.83 q-147.67 -121.83 -211.65 -200.59
                         q-51.69 -68.91 -67.69 -119.97 q-16 -51.06 -14.77 -116.29 q3.69 -146.44 91.06 -244.88 q87.37 -98.45 220.27 -102.14
                         q56.6 0 107.67 20.3 q51.07 20.3 92.91 58.46 q41.84 -38.15 92.91 -58.46 q51.07 -20.3 107.67 -20.3
                         q132.9 3.69 220.27 102.14 q87.37 98.45 91.06 244.88 q1.23 61.53 -14.76 113.21 q-16 51.68 -67.68 123.05
                         q-63.98 76.3 -212.88 199.36 q-148.9 123.06 -189.51 155.06 q-6.15 2.46 -13.54 4.92 q-7.38 2.46 -13.53 2.46 ZM322.49 78.76
                         q-97.21 3.69 -168.58 85.52 q-71.37 81.83 -75.07 197.5 q-1.23 35.69 8 79.99 q9.23 44.3 51.07 105.83 q44.3 52.91 163.66 148.9
                         l210.43 169.81 l210.43 -169.81 q119.36 -95.99 163.66 -148.9 q43.07 -61.53 52.3 -107.06 q9.23 -45.53 6.77 -78.76
                         q-3.7 -115.67 -75.07 -197.5 q-71.37 -81.83 -168.58 -85.52 q-49.23 0 -87.38 21.53 q-38.14 21.53 -78.75 57.21
                         q-11.07 8.62 -17.23 10.47 q-6.15 1.84 -6.15 1.84 l-4.92 -1.23 q-6.16 -1.23 -14.77 -7.39 q-43.07 -35.68 -81.83 -58.44
                         q-38.76 -22.77 -87.99 -23.99 Z",
                Fill = "#FF000000"
            };

            ToView = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M837.18 92.91 q38.96 1 65.44 27.48 q26.48 26.48 27.48 65.43 l0 651.36 q-1 39.96 -27.48 65.94
                         q-26.48 25.98 -65.44 26.98 l-651.36 0 q-38.96 -1 -65.43 -26.98 q-26.48 -25.98 -27.48 -65.94 l0 -651.36
                         q1 -38.96 27.48 -65.43 q26.48 -26.48 65.43 -27.48 l651.36 0 ZM837.18 0 l-651.36 0 q-78.92 2 -131.37 54.45
                         q-52.45 52.45 -54.45 131.37 l0 651.36 q2 78.93 54.45 131.38 q52.45 52.44 131.37 54.44 l651.36 0 q78.93 -2 131.88 -54.44
                         q52.94 -52.45 54.94 -131.38 l0 -651.36 q-3 -78.92 -55.44 -131.37 q-52.45 -52.45 -131.38 -54.45 ZM697.32 744.27
                         q-12.99 0 -24.98 -7.99 l-242.76 -159.84 q-20.98 -13.99 -20.98 -38.96 l0 -278.73 q0 -19.98 12.99 -33.47
                         q12.99 -13.49 32.97 -13.49 q19.98 0 32.96 13.49 q12.99 13.49 13.99 33.47 l0 252.75 l221.78 143.86
                         q17.99 7.99 24.98 24.98 q6.99 16.99 0 35.96 q-6.99 14.99 -20.98 22.48 q-13.99 7.49 -29.97 5.49 Z",
                Fill = "#FF000000"
            };

            History = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M512 0 q-217 6 -361.5 150.5 q-144.5 144.5 -150.5 361.5 q6 217 150.5 361.5 q144.5 144.5 361.5 150.5
                         q217 -6 361.5 -150.5 q144.5 -144.5 150.5 -361.5 q-6 -217 -150.5 -361.5 q-144.5 -144.5 -361.5 -150.5 ZM512 939
                         q-183 -5 -302.5 -124.5 q-119.5 -119.5 -124.5 -302.5 q5 -183 124.5 -302.5 q119.5 -119.5 302.5 -124.5
                         q183 5 302.5 124.5 q119.5 119.5 124.5 302.5 q-5 183 -124.5 302.5 q-119.5 119.5 -302.5 124.5 ZM512 256
                         l-85 0 l0 341 l341 0 l0 -85 l-256 0 l0 -256 Z",
                Fill = "#FF000000"
            };

            VideoUp = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M711,212.3l47-47c41.7-47.6-25-112.2-72-71l-107,106c-3.3,3.3-6,7-8,11H454c-1.3-2.7-3.3-5-6-7l-112-111
                         c-21.3-17.3-51-17-71,3s-20.3,49.7-3,71l45,44H198c-56.7,0-105,48.3-105,105v417c0,56.7,48.3,105,105,105h57
                         c-17.9,49.6,19.9,105,73.5,105c55.4,0,90.4-55.3,74.5-105h218c-15.9,49.7,19.1,105,74.5,105c53.6,0,91.4-55.4,73.5-105h57
                         c56.7,0,105-48.3,105-105v-417c0-57.6-48.3-104-105-104H711L711,212.3z",
                Fill = "#FF000000"
            };

            Channel = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M921.6 211.41 q1.1 -6.61 7.15 -10.47 q6.05 -3.85 12.66 -0.54 q38.54 19.82 60.56 56.16
                         q22.02 36.33 22.02 80.38 l0 329.22 q-1.1 64.96 -44.6 108.46 q-43.5 43.5 -108.45 44.6
                         l-483.37 0 q-44.04 0 -80.38 -22.02 q-36.34 -22.02 -56.15 -61.66 q-3.31 -5.51 0.54 -11.57
                         q3.85 -6.05 10.47 -7.15 l557.14 0 q44.04 -1.1 72.67 -29.73 q28.63 -28.63 29.73 -72.67
                         l0 -402.99 ZM102.4 0 l666.15 0 q41.84 0 71.57 29.73 q29.73 29.73 30.83 72.67 l0 461.35
                         q-1.1 41.84 -30.83 71.57 q-29.73 29.73 -71.57 30.83 l-666.15 0 q-42.94 -1.1 -72.67 -30.83
                         q-29.73 -29.73 -29.73 -71.57 l0 -461.35 q0 -42.94 29.73 -72.67 q29.73 -29.73 72.67 -29.73
                         ZM559.35 353.45 q9.91 -7.71 9.91 -20.93 q0 -13.21 -9.91 -20.92 l-186.09 -129.92 q-13.21 -8.81 -26.42 -1.66
                         q-13.21 7.16 -14.32 22.57 l0 259.85 q1.11 15.42 14.32 22.57 q13.21 7.16 26.42 -1.65 l186.09 -129.92 Z",
                Fill = "#FF000000"
            };

            Channel1 = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M116.57 0 q-30.86 0 -57.71 16 q-26.85 16 -42.85 42.85 q-16 26.85 -16 57.71 l0 468.57 q0 30.86 16 57.72
                         q16 26.86 42.85 42.86 q26.85 16 57.71 16 l643.43 0 q32 0 58.86 -16 q26.86 -16 42.86 -42.86
                         q16 -26.86 16 -57.72 l0 -468.57 q0 -30.86 -16 -57.71 q-16 -26.85 -42.86 -42.85 q-26.86 -16 -58.86 -16
                         l-643.43 0 ZM262.86 848 q-40 0 -72.57 -25.71 q-32.58 -25.72 -39.43 -62.29 l697.14 0 q36.57 0 62.29 -25.71
                         q25.71 -25.72 25.71 -62.29 l0 -521.14 q37.71 11.43 62.86 42.28 q25.14 30.86 25.14 69.72 l0 468.57
                         q0 30.86 -16 57.72 q-16 26.86 -42.86 42.86 q-26.86 16 -58.86 16 l-643.43 0 ZM88 174.86
                         q0 -36.57 25.14 -62.29 q25.14 -25.71 61.72 -25.71 l526.85 0 q36.58 0 62.29 25.71 q25.71 25.72 25.71 62.29
                         l0 350.85 q0 36.58 -25.71 62.29 q-25.71 25.71 -62.29 25.71 l-526.85 0 q-36.57 0 -61.72 -25.71
                         q-25.14 -25.71 -25.14 -62.29 l0 -350.85 ZM609.14 374.86 q11.43 -6.86 11.43 -22.29 q0 -15.43 -11.43 -26.86
                         l-214.85 -146.28 q-13.72 -10.29 -28.57 -1.72 q-14.86 8.58 -14.86 26.86 l0 297.14 q0 18.29 14.86 26.86
                         q14.85 8.57 28.57 -2.86 l214.85 -150.85 Z",
                Fill = "#FF000000"
            };

            SeasonsSeries = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M974.2 382.22 l-401.47 177.37 q-28.88 13.75 -60.5 13.75 q-31.62 0 -60.5 -13.75 l-400.09 -177.37
                         q-20.63 -11 -33 -30.25 q-12.38 -19.25 -13.06 -43.31 q-0.68 -24.06 10.32 -45.38 q11 -21.32 31.62 -35.07
                         l402.85 -210.36 q28.87 -17.87 63.25 -17.87 q34.38 0 63.25 19.25 l398.72 207.61 q22 12.37 33.69 34.37
                         q11.68 22 10.31 46.06 q-1.38 24.06 -13.06 44 q-11.69 19.94 -32.31 30.94 ZM939.83 292.86 l-400.1 -207.62
                         q-11 -8.25 -24.75 -9.62 q-16.5 1.37 -30.25 9.62 l-400.1 207.62 q-2.75 6.87 -2.75 12.37 q0 5.5 4.13 9.62
                         l397.35 175.99 q20.62 9.63 28.18 9.63 q7.56 0 26.81 -8.25 l404.22 -178.74 q1.38 -11 -4.12 -20.62
                         l1.38 0 ZM20.01 512.84 q8.25 -4.12 19.25 -4.12 q11 0 19.25 5.5 q16.5 9.62 39.87 20.62 l413.85 186.99
                         l394.6 -178.74 l60.49 -30.25 q9.63 -4.12 19.94 -4.12 q10.31 0 19.25 5.5 q8.94 5.5 13.75 15.12
                         q4.81 9.63 3.43 19.25 q-1.37 23.37 -21.99 31.62 l-64.62 31.62 l-424.85 193.87 l-422.1 -192.49
                         q-35.74 -12.38 -68.74 -33 q-9.63 -5.5 -15.13 -14.43 q-5.5 -8.94 -6.18 -19.25 q-0.69 -10.32 4.82 -19.25
                         q5.5 -8.94 15.12 -14.44 l0 0 ZM58.51 732.83 q17.87 9.62 41.25 20.62 l412.47 186.99 l394.6 -178.74
                         l60.49 -30.25 q9.63 -5.5 19.94 -4.81 q10.31 0.69 19.25 6.18 q8.94 5.5 13.75 15.12 q4.81 9.63 3.43 19.94
                         q-1.37 10.32 -6.87 18.57 q-5.5 8.25 -15.12 12.37 l-64.62 31.62 l-424.85 192.49 l-422.1 -191.11
                         q-35.74 -13.75 -68.74 -34.37 q-13.75 -6.88 -17.88 -22 q-4.13 -15.13 3.44 -28.87 q7.56 -13.75 22.68 -17.87
                         q15.13 -4.13 28.88 4.13 Z",
                Fill = "#FF000000"
            };

            PlatformIpad = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"M804.57 0 q40 0 73.72 19.43 q33.71 19.43 53.14 53.15 q19.43 33.72 19.43 73.71 l0 731.42 q0 40 -19.43 73.71
                         q-19.43 33.72 -53.14 53.14 q-33.72 19.43 -73.72 19.43 l-585.14 0 q-40 0 -73.72 -19.43 q-33.72 -19.43 -53.15 -53.14
                         q-19.43 -33.71 -19.43 -73.71 l0 -731.42 q0 -40 19.43 -73.71 q19.43 -33.72 53.15 -53.15 q33.72 -19.43 73.72 -19.43
                         l585.14 0 ZM512 877.71 q-22.86 0 -38.86 16 q-16 16 -16 38.86 q0 22.86 16 38.86 q16 16 38.86 16 q22.86 0 38.86 -16
                         q16 -16 16 -38.86 q0 -22.86 -16 -38.86 q-16 -16 -38.86 -16 ZM804.57 73.14 l-585.14 0 q-27.43 0 -48.57 18.28
                         q-21.15 18.28 -24.57 46.86 l0 8 l0 621.71 q0 27.43 18.28 48.57 q18.29 21.14 46.86 24.57 l8 0 l585.14 0
                         q27.43 0 48.57 -18.28 q21.14 -18.28 24.57 -46.86 l0 -8 l0 -621.71 q0 -30.86 -21.14 -52 q-21.14 -21.14 -52 -21.14 Z",
                Fill = "#FF000000"
            };

            PlatformMobile = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"M694.86 0 q40 0 73.72 19.43 q33.71 19.43 53.14 53.15 q19.43 33.72 19.43 73.71 l0 731.42 q0 40 -19.43 73.71
                         q-19.42 33.72 -53.14 53.14 q-33.72 19.43 -73.72 19.43 l-365.72 0 q-40 0 -73.71 -19.43 q-33.72 -19.43 -53.14 -53.14
                         q-19.43 -33.71 -19.43 -73.71 l0 -731.42 q0 -40 19.43 -73.71 q19.42 -33.72 53.14 -53.15 q33.71 -19.43 73.71 -19.43
                         l365.72 0 ZM512 877.71 q-22.86 0 -38.86 16 q-16 16 -16 38.86 q0 22.86 16 38.86 q16 16 38.86 16 q22.86 0 38.86 -16
                         q16 -16 16 -38.86 q0 -22.86 -16 -38.86 q-16 -16 -38.86 -16 ZM694.86 73.14 l-365.72 0 q-27.43 0 -48.57 18.28
                         q-21.14 18.28 -24.57 46.86 l0 8 l0 621.71 q0 27.43 18.28 48.57 q18.29 21.14 46.86 24.57 l8 0 l365.72 0
                         q27.43 0 48.57 -18.28 q21.14 -18.28 24.57 -46.86 l0 -8 l0 -621.71 q0 -30.86 -21.14 -52 q-21.14 -21.14 -52 -21.14 Z",
                Fill = "#FF000000"
            };

            PlatformPC = new VectorImage
            {
                Height = 32,
                Width = 42,
                Data = @"M694.86 804.57 q14.85 0 25.71 10.86 q10.86 10.86 10.86 25.71 q0 14.86 -10.86 25.71 q-10.86 10.86 -25.71 10.86
                         l-365.72 0 q-14.85 0 -25.71 -10.86 q-10.86 -10.86 -10.86 -25.71 q0 -14.86 10.86 -25.71 q10.86 -10.86 25.71 -10.86
                         l365.72 0 ZM877.71 0 q40 0 73.71 19.43 q33.72 19.43 53.14 53.15 q19.43 33.72 19.43 73.71 l0 438.85 q0 40 -19.43 73.72
                         q-19.43 33.72 -53.14 53.14 q-33.71 19.43 -73.71 19.43 l-731.42 0 q-40 0 -73.71 -19.43 q-33.72 -19.43 -53.15 -53.14
                         q-19.43 -33.72 -19.43 -73.72 l0 -438.85 q0 -40 19.43 -73.71 q19.43 -33.72 53.15 -53.15 q33.72 -19.43 73.71 -19.43
                         l731.42 0 ZM877.71 73.14 l-731.42 0 q-27.43 0 -48.57 18.28 q-21.15 18.28 -24.58 46.86 l0 8 l0 438.85
                         q0 27.43 18.28 48.58 q18.28 21.14 46.86 24.57 l8 0 l731.42 0 q27.43 0 48.57 -18.29 q21.14 -18.29 24.58 -46.86
                         l0 -8 l0 -438.85 q0 -30.86 -21.14 -52 q-21.14 -21.14 -52 -21.14 ZM347.43 182.86 q34.28 0 63.99 17.14
                         q29.72 17.14 46.86 46.85 q17.15 29.72 17.15 64 q0 34.28 -17.15 64 q-17.14 29.71 -46.86 46.86
                         q-29.71 17.14 -63.99 17.14 l-91.43 0 l0 109.71 l-73.14 0 l0 -365.71 l164.57 0 ZM694.86 182.86
                         q44.57 0 82.85 19.43 q38.29 19.42 63.43 53.71 l-60.57 41.14 q-22.86 -27.43 -56 -37.14 q-33.14 -9.71 -66.28 2.29
                         q-33.14 12 -53.14 40.57 q-20 28.57 -20 62.86 q0 34.28 20 62.85 q20 28.57 53.14 40.57 q33.14 12 66.28 2.28
                         q33.14 -9.71 56 -37.13 l60.57 41.14 q-38.85 51.43 -101.14 67.43 q-62.29 16 -121.14 -10.86
                         q-58.86 -26.86 -87.42 -84.58 q-28.57 -57.71 -14.86 -120.57 q13.71 -62.86 64 -103.43 q50.28 -40.57 114.28 -40.57
                         ZM347.43 256 l-91.43 0 l0 109.71 l91.43 0 q22.86 0 38.86 -16 q16 -16 16 -38.85 q0 -22.86 -16 -38.86 q-16 -16 -38.86 -16 Z",
                Fill = "#FF000000"
            };

            PlatformTV = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"M768 804.57 q14.86 0 25.72 10.86 q10.86 10.86 10.86 25.71 q0 14.86 -10.86 25.71 q-10.86 10.86 -25.72 10.86
                         l-512 0 q-14.86 0 -25.72 -10.86 q-10.85 -10.86 -10.85 -25.71 q0 -14.86 10.85 -25.71 q10.85 -10.86 25.72 -10.86
                         l512 0 ZM877.71 0 q40 0 73.71 19.43 q33.72 19.43 53.14 53.15 q19.43 33.72 19.43 73.71 l0 438.85 q0 40 -19.43 73.72
                         q-19.43 33.72 -53.14 53.14 q-33.71 19.43 -73.71 19.43 l-731.42 0 q-40 0 -73.71 -19.43 q-33.72 -19.43 -53.15 -53.14
                         q-19.43 -33.72 -19.43 -73.72 l0 -438.85 q0 -40 19.43 -73.71 q19.43 -33.72 53.15 -53.15 q33.72 -19.43 73.71 -19.43
                         l731.42 0 ZM877.71 73.14 l-731.42 0 q-27.43 0 -48.57 18.28 q-21.15 18.28 -24.58 46.86 l0 8 l0 438.85
                         q0 27.43 18.28 48.58 q18.28 21.14 46.86 24.57 l8 0 l731.42 0 q27.43 0 48.57 -18.29 q21.14 -18.29 24.58 -46.86
                         l0 -8 l0 -438.85 q0 -30.86 -21.14 -52 q-21.14 -21.14 -52 -21.14 ZM585.14 182.86 l109.72 274.28 l109.71 -274.28
                         l73.14 0 l-146.28 365.71 l-73.14 0 l-116.58 -292.57 l-176 0 l0 292.57 l-73.14 0 l0 -292.57 l-146.28 0 l0 -73.14 l438.85 0 Z",
                Fill = "#FF000000"
            };

            UpzhuIcon = new VectorImage
            {
                Height = 14,
                Width = 14,
                Data = @"m896,736l0,-448c0,-54.4 -41.6,-96 -96,-96l-576,0c-54.4,0 -96,41.6 -96,96l0,448c0,54.4 41.6,96 96,96l576,0c54.4,0 
                        96,-41.6 96,-96zm-96,-608c89.6,0 160,70.4 160,160l0,448c0,89.6 -70.4,160 -160,160l-576,0c-89.6,0 
                        -160,-70.4 -160,-160l0,-448c0,-89.6 70.4,-160 160,-160l576,0zm-380.8,416l0,-217.6l60.8,0l0,240c0,96 -57.6,144 
                        -147.2,144s-140.8,-44.8 -140.8,-140.8l0,-243.2l60.8,0l0,217.6c0,51.2 3.2,108.8 83.2,108.8s83.2,-57.6 
                        83.2,-108.8zm288,-38.4c28.8,0 60.8,-16 60.8,-60.8c0,-48 -28.8,-60.8 -60.8,-60.8l-92.8,0l0,121.6l92.8,0zm3.2,-179.2c102.4,0 
                        121.6,70.4 121.6,115.2c0,48 -19.2,115.2 -121.6,115.2l-96,0l0,147.2l-60.8,0l0,-377.6l156.8,0z",
                Fill = "#FF000000"
            };

            Vidicon = new VectorImage
            {
                Height = 32,
                Width = 32,
                Data = @"m640,490.66667l-72.53333,0c29.86666,-21.33334 51.2,-55.46667 51.2,-93.86667c0,-59.73333 -46.93334,-106.66667 -102.4,-106.66667c-59.73334,0 
                        -102.4,46.93334 -102.4,106.66667c0,38.4 17.06666,72.53333 46.93333,89.6l-140.8,0c51.2,-25.6 89.6,-85.33333 89.6,-149.33333c0,-89.6 
                        -72.53333,-166.4 -162.13333,-166.4s-162.13334,76.8 -162.13334,166.4c0,64 34.13334,123.73333 89.6,149.33333l-51.2,0c-12.8,0 -25.6,12.8 
                        -25.6,25.6l0,311.46667c0,17.06666 12.8,25.6 25.6,25.6l516.26667,0c12.8,0 25.6,-12.8 25.6,-25.6l0,-307.2c0,-17.06667 -8.53333,-25.6 
                        -25.6,-25.6zm34.13333,273.06666l55.46667,0l0,-187.73333l-55.46667,0l0,187.73333zm64,-187.73333l0,187.73333l200.53334,89.6l0,-366.93333l-200.53334,89.6z",
                Fill = "#FF000000"
            };

            VerticalScreen = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"m705.42222,27.30667l-405.04889,0c-61.44,0 -111.50222,50.06222 -111.50222,111.50222l0,735.00444c0,61.44 
                        50.06222,111.50223 111.50222,111.50223l405.04889,0c61.44,0 111.50222,-50.06223 111.50222,-111.50223l0,-735.00444c0,-61.44 
                        -50.06222,-111.50222 -111.50222,-111.50222zm-238.93333,36.40889l72.81778,0c9.10222,0 18.20444,9.10222 
                        18.20444,18.20444s-9.10222,18.20444 -18.20444,18.20444l-72.81778,0c-9.10222,0 -18.20445,-9.10222 
                        -18.20445,-18.20444s9.10223,-18.20444 18.20445,-18.20444zm36.40889,876.08888c-25.03111,0 -47.78667,-20.48 
                        -47.78667,-47.78666c0,-25.03111 20.48,-47.78667 47.78667,-47.78667c25.03111,0 47.78666,20.48 47.78666,47.78667c0,27.30666 
                        -20.48,47.78666 -47.78666,47.78666zm238.93333,-152.46222l-477.86667,0l0,-20.48l477.86667,0l0,20.48z",
                Fill = "#FF000000"

            };

            HorizontalScreen = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"m992,128l-960,0c-17.7,0 -32,14.3 -32,32l0,576c0,17.7 14.3,32 32,32l348,0c2.2,0 4,1.8 4,4l0,108l-124,0c-2.2,0 -4,1.8 -4,4l0,40c0,2.2 
                        1.8,4 4,4l504,0c2.2,0 4,-1.8 4,-4l0,-40c0,-2.2 -1.8,-4 -4,-4l-124,0l0,-108c0,-2.2 1.8,-4 4,-4l348,0c17.7,0 32,-14.3 
                        32,-32l0,-576c0,-17.7 -14.3,-32 -32,-32zm-416,752l-128,0l0,-96c0,-8.8 7.2,-16 16,-16l96,0c8.8,0 16,7.2 16,16l0,96z",
                Fill = "#FF000000"
            };

            UnKnownIcon = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"m492.8,516.8l-60,180l24.8,-2.4c28,-2.4 22.4,10.4 97.6,-224.8l43.2,-133.6l-22.4,0c-22.4,0 -23.2,2.4 
                        -83.2,180.8zm-348.8,-0.8l0,164l64,0l0,-212l70.4,105.6c68.8,104.8 70.4,106.4 
                        100,106.4l29.6,0l0,-328l-64,0l0,212l-70.4,-105.6c-68.8,-104.8 -70.4,-106.4 -99.2,-106.4l-30.4,0l0,164zm576,-146.4c-4,10.4 
                        -30.4,84 -58.4,164l-51.2,146.4l34.4,0c32.8,0 34.4,-0.8 45.6,-34.4l11.2,-33.6l116.8,0l8.8,32c8.8,30.4 11.2,32 43.2,34.4c18.4,1.6 
                        33.6,0 33.6,-4c0,-3.2 -24.8,-76.8 -55.2,-162.4l-54.4,-156l-32.8,-2.4c-28.8,-2.4 -34.4,0 -41.6,16zm60,120c7.2,27.2 15.2,54.4 
                        17.6,60c3.2,8 -6.4,10.4 -37.6,10.4c-36,0 -41.6,-1.6 -37.6,-14.4c1.6,-7.2 10.4,-36 17.6,-63.2c8,-27.2 16.8,-48 
                        20,-46.4c3.2,1.6 12,25.6 20,53.6z",
                Fill = "#FF000000"
            };

            Loding = new VectorImage
            {
                Height = 16,
                Width = 16,
                Data = @"m144.205,202.496a136.678,136.678 0 1 0 273.357,0a136.678,136.678 0 1 0 -273.357,0zm-102.477,290.406a119.578,119.578 
                        0 1 0 239.155,0a119.578,119.578 0 1 0 -239.155,0zm102.502,256.256a102.502,102.502 0 1 0 205.005,0a102.502,102.502 
                        0 1 0 -205.005,0zm290.97,112.768a89.6,89.6 0 1 0 179.2,0a89.6,89.6 0 1 0 -179.2,0zm289.843,-95.666a85.427,85.427 
                        0 1 0 170.855,0a85.427,85.427 0 1 0 -170.855,0zm136.704,-290.433a68.326,68.326 0 1 0 136.653,0a68.326,68.326 0 1 
                        0 -136.653,0zm-102.527,-256.256a51.251,51.251 0 1 0 102.502,0a51.251,51.251 0 1 0 
                        -102.503,0l0.001,0zm-247.22,-134.195a34.176,34.176 0 1 0 68.352,0a34.176,34.176 0 1 0 -68.352,0z",
                Fill = "#FF000000"
            };
        }
        /// <summary>
        /// UP主- 圆角矩形中 UP
        /// </summary>
        public VectorImage UpzhuIcon { get; private set; }
        /// <summary>
        /// 播放
        /// </summary>
        public VectorImage Play { get; private set; }
        /// <summary>
        /// 喜欢
        /// </summary>
        public VectorImage Like { get; private set; }
        /// <summary>
        /// 收藏
        /// </summary>
        public VectorImage Favorite { get; private set; }
        /// <summary>
        /// 分享
        /// </summary>
        public VectorImage Share { get; private set; }
        /// <summary>
        /// 云下载  云朵里面有下载图标
        /// </summary>
        public VectorImage CloudDownload { get; private set; }
        /// <summary>
        /// 文件夹
        /// </summary>
        public VectorImage Folder { get; private set; }
        /// <summary>
        /// 下载中
        /// </summary>
        public VectorImage Downloading { get; private set; }
        /// <summary>
        /// 已下载
        /// </summary>
        public VectorImage DownloadFinished { get; private set; }
        /// <summary>
        /// 投币图   圈中是个“币”
        /// </summary>
        public VectorImage CoinIcon { get; private set; }
        /// <summary>
        /// 投币图   圈中是个 “B”
        /// </summary>
        public VectorImage MoneyIcon { get; private set; }
        public VectorImage BindingEmail { get; private set; }
        public VectorImage BindingPhone { get; private set; }

        public VectorImage FavoriteOutline { get; private set; }
        public VectorImage Subscription { get; private set; }
        public VectorImage ToView { get; private set; }
        public VectorImage History { get; private set; }

        public VectorImage VideoUp { get; private set; }
        public VectorImage Channel { get; private set; }
        public VectorImage Channel1 { get; private set; }
        public VectorImage SeasonsSeries { get; private set; }
        /// <summary>
        /// Ipad平台
        /// </summary>
        public VectorImage PlatformIpad { get; private set; }
        public VectorImage PlatformMobile { get; private set; }
        public VectorImage PlatformPC { get; private set; }
        public VectorImage PlatformTV { get; private set; }
        /// <summary>
        /// 摄像机图像
        /// </summary>
        public VectorImage Vidicon { get; private set; }
        /// <summary>
        /// 竖屏   直立的手机
        /// </summary>
        public VectorImage VerticalScreen { get; set; }
        /// <summary>
        /// 横屏   躺平的手机
        /// </summary>
        public VectorImage HorizontalScreen { get; set; }
        public VectorImage UnKnownIcon { get; set; }

        public VectorImage Loding { get; set; }
    }
}
