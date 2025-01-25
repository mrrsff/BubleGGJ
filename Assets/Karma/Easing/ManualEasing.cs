using UnityEngine;

namespace Karma.Easing
{
    public static class ManualEasing
    {
        private const float TwoPi = Mathf.PI * 2f;
        
        public delegate float Easer(float t);
        
        public static float Linear(float t)
        {
            return t;
        }

        private static float Spring(float t)
        {
            t = Mathf.Clamp01(t);
            t = (Mathf.Sin(t * Mathf.PI * (0.2f + 2.5f * t * t * t)) * Mathf.Pow(1f - t, 2.2f) + t) *
                (1f + (1.2f * (1f - t)));
            return t;
        }

        public static class Quadratic
        {
            public static float In(float t)
            {
                return t * t;
            }

            public static float Out(float t)
            {
                return t * (2f - t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f)
                {
                    return 0.5f * t * t;
                }

                return -0.5f * ((t -= 1f) * (t - 2f) - 1f);
            }
        }

        public static class Cubic
        {
            public static float In(float t)
            {
                return t * t * t;
            }

            public static float Out(float t)
            {
                return 1f + ((t -= 1f) * t * t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f)
                {
                    return 0.5f * t * t * t;
                }

                return 0.5f * ((t -= 2f) * t * t + 2f);
            }
        }

        public static class Quartic
        {
            public static float In(float t)
            {
                return t * t * t * t;
            }

            public static float Out(float t)
            {
                return 1f - ((t -= 1f) * t * t * t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f)
                {
                    return 0.5f * t * t * t * t;
                }

                return -0.5f * ((t -= 2f) * t * t * t - 2f);
            }
        }

        public static class Quintic
        {
            public static float In(float t)
            {
                return t * t * t * t * t;
            }

            public static float Out(float t)
            {
                return 1f + ((t -= 1f) * t * t * t * t);
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f)
                {
                    return 0.5f * t * t * t * t * t;
                }

                return 0.5f * ((t -= 2f) * t * t * t * t + 2f);
            }
        }

        public static class Sine
        {
            public static float In(float t)
            {
                return 1f - Mathf.Cos(t * Mathf.PI / 2f);
            }

            public static float Out(float t)
            {
                return Mathf.Sin(t * Mathf.PI / 2f);
            }

            public static float InOut(float t)
            {
                return 0.5f * (1f - Mathf.Cos(Mathf.PI * t));
            }
        }

        public static class Exponential
        {
            public static float In(float t)
            {
                return NumberExtensions.Approximately(t, 0f) ? 0f : Mathf.Pow(1024f, t - 1f);
            }

            public static float Out(float t)
            {
                return NumberExtensions.Approximately(t, 1f) ? 1f : 1f - Mathf.Pow(2f, -10f * t);
            }

            public static float InOut(float t)
            {
                if (NumberExtensions.Approximately(t, 0f))
                {
                    return 0f;
                }

                if (NumberExtensions.Approximately(t, 1f))
                {
                    return 1f;
                }

                if ((t *= 2f) < 1f)
                {
                    return 0.5f * Mathf.Pow(1024f, t - 1f);
                }

                return 0.5f * (-Mathf.Pow(2f, -10f * (t - 1f)) + 2f);
            }
        }
        
        public static class Circular
        {
            public static float In(float t)
            {
                return 1f - Mathf.Sqrt(1f - t * t);
            }

            public static float Out(float t)
            {
                return Mathf.Sqrt(1f - ((t -= 1f) * t));
            }

            public static float InOut(float t)
            {
                if ((t *= 2f) < 1f)
                {
                    return -0.5f * (Mathf.Sqrt(1f - t * t) - 1);
                }

                return 0.5f * (Mathf.Sqrt(1f - (t -= 2f) * t) + 1f);
            }
        }

        public static class Elastic
        {
            const float S = 1.70158f;

            public static float In(float t)
            {
                return In(t,1,0);
            }
            
            public static float Out(float t)
            {
                return Out(t,1,0);
            }
            
            public static float InOut(float t )
            {
                return InOut(t,1,0);
            }

            public static float In(float t, float overshootAmplitude = 1, float period = 0)
            {
                float s = overshootAmplitude * S;
                float s1;
                if (t == 0)
                {
                    return 0;
                }

                if ((t /= 1) == 1)
                {
                    return 1;
                }

                if (period == 0)
                {
                    period = 0.3f;
                }

                if (s < 1)
                {
                    s = 1;
                    s1 = period / 4;
                }
                else
                {
                    s1 = period / TwoPi * Mathf.Asin(1 / s);
                }

                return -(s * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t - s1) * TwoPi / period));
            }

            public static float Out(float t, float overshootAmplitude = 1, float period = 0)
            {
                float s = overshootAmplitude * S;
                float s1;
                if (t == 0)
                {
                    return 0;
                }

                if ((t /= 1) == 1)
                {
                    return 1;
                }

                if (period == 0)
                {
                    period = 0.3f;
                }

                if (s < 1)
                {
                    s = 1;
                    s1 = period / 4;
                }
                else
                {
                    s1 = period / TwoPi * Mathf.Asin(1 / s);
                }

                return (s * Mathf.Pow(2, -10 * t) *
                        Mathf.Sin((t - s1) * TwoPi / period) + 1);
            }

            public static float InOut(float t, float overshootAmplitude = 1, float period = 0)
            {
                float s = overshootAmplitude * S;
                float s1;
                
                if (t == 0)
                {
                    return 0;
                }

                if ((t /= 0.5f) == 2)
                {
                    return 1;
                }

                if (period == 0)
                {
                    period = (0.3f * 1.5f);
                }

                if (s < 1)
                {
                    s = 1;
                    s1 = period / 4;
                }
                else
                {
                    s1 = period / TwoPi * Mathf.Asin(1 / s);
                }

                if (t < 1)
                {
                    return -0.5f * (s * Mathf.Pow(2, 10 * (t -= 1)) * 
                                    Mathf.Sin((t - s1) * TwoPi / period));
                }

                return s * Mathf.Pow(2, -10 * (t -= 1)) *
                       Mathf.Sin((t - s1) * TwoPi / period) * 0.5f + 1;
            }
        }

        public static class Back
        {
            const float S = 1.70158f;
            const float S2 = 2.5949095f;

            public static float In(float t)
            {
                return t * t * ((S + 1f) * t - S);
            }
            
            public static float In(float t, float overshootAmplitude)
            {
                var s = S * overshootAmplitude;
                return t * t * ((s + 1f) * t - s);
            }

            public static float Out(float t, float overshootAmplitude)
            {
                var s = S * overshootAmplitude;
                return (t -= 1f) * t * ((s + 1f) * t + s) + 1f;
            }
            
            public static float Out(float t)
            {
                return Out(t, 1);
            }
            
            public static float InOut(float t)
            {
                return InOut(t, 1);
            }

            public static float InOut(float t, float overshootAmplitude)
            {            
                var s2 = S2 * overshootAmplitude;

                if ((t *= 2f) < 1f)
                {
                    return 0.5f * (t * t * ((s2 + 1f) * t - s2));
                }

                return 0.5f * ((t -= 2f) * t * ((s2 + 1f) * t + s2) + 2f);
            }
        }

        public static class Bounce
        {
            public static float In(float t)
            {
                return 1f - Out(1f - t);
            }

            public static float Out(float t)
            {
                if (t < (1f / 2.75f))
                {
                    return 7.5625f * t * t;
                }

                if (t < (2f / 2.75f))
                {
                    return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
                }
           
                if (t < (2.5f / 2.75f))
                {
                    return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
                }

                return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
            }

            public static float InOut(float t)
            {
                if (t < 0.5f)
                {
                    return In(t * 2f) * 0.5f;
                }

                return Out(t * 2f - 1f) * 0.5f + 0.5f;
            }
        }

        public static Easer GetEase(ManualEasingType easeType)
        {
            switch (easeType)
            {                
                case ManualEasingType.Linear:
                    return Linear;
                case ManualEasingType.Spring:
                    return Spring;
                case ManualEasingType.SineOut:
                    return Sine.Out;
                case ManualEasingType.SineIn:
                    return Sine.In;
                case ManualEasingType.SineInOut:
                    return Sine.InOut;
                case ManualEasingType.QuadIn:
                    return Quadratic.In;
                case ManualEasingType.QuadOut:
                    return Quadratic.Out;
                case ManualEasingType.QuadInOut:
                    return Quadratic.InOut;
                case ManualEasingType.CubicIn:
                    return Cubic.In;
                case ManualEasingType.CubicOut:
                    return Cubic.Out;
                case ManualEasingType.CubicInOut:
                    return Cubic.InOut;
                case ManualEasingType.QuartIn:
                    return Quartic.In;
                case ManualEasingType.QuartOut:
                    return Quartic.Out;
                case ManualEasingType.QuartInOut:
                    return Quartic.InOut;
                case ManualEasingType.QuintIn:
                    return Quintic.In;
                case ManualEasingType.QuintOut:
                    return Quintic.Out;
                case ManualEasingType.QuintInOut:
                    return Quintic.InOut;
                case ManualEasingType.ExpoIn:
                    return Exponential.In;
                case ManualEasingType.ExpoOut:
                    return Exponential.Out;
                case ManualEasingType.ExpoInOut:
                    return Exponential.InOut;
                case ManualEasingType.CircIn:
                    return Circular.In;
                case ManualEasingType.CircOut:
                    return Circular.Out;
                case ManualEasingType.CircInOut:
                    return Circular.InOut;
                case ManualEasingType.BounceIn:
                    return Bounce.In;
                case ManualEasingType.BounceOut:
                    return Bounce.Out;
                case ManualEasingType.BounceInOut:
                    return Bounce.InOut;
                case ManualEasingType.BackIn:
                    return Back.In;
                case ManualEasingType.BackOut:
                    return Back.Out;
                case ManualEasingType.BackInOut:
                    return Back.InOut;
                case ManualEasingType.ElasticIn:
                    return Elastic.In;
                case ManualEasingType.ElasticOut:
                    return Elastic.Out;
                case ManualEasingType.ElasticInOut:
                    return Elastic.InOut;
                default:
                    return Linear;
            }
        }
    }

    public enum ManualEasingType
    {
        Linear,
        Spring,
        SineIn,
        SineOut,
        SineInOut,
        QuadIn,
        QuadOut,
        QuadInOut,
        CubicIn,
        CubicOut,
        CubicInOut,
        QuartIn,
        QuartOut,
        QuartInOut,
        QuintIn,
        QuintOut,
        QuintInOut,
        ExpoIn,
        ExpoOut,
        ExpoInOut,
        CircIn,
        CircOut,
        CircInOut,
        BounceIn,
        BounceOut,
        BounceInOut,
        BackIn,
        BackOut,
        BackInOut,
        ElasticIn,
        ElasticOut,
        ElasticInOut
    }
    
    public static class NumberExtensions
    {
        public static bool Approximately(float a, float b, float epsilon = 0.0001f)
        {
            return Mathf.Abs(a - b) < epsilon;
        }
    }
}