namespace MyLib;

public class LibBaiTap
{
    public static int GiaiPTBac2(
        double a, double b, double c,
        ref double x1, ref double x2)
    {
        const double EPS = 1e-9;

        int sn;

        if (Math.Abs(a) < EPS)
        {
            if (Math.Abs(b) < EPS)
            {
                if (Math.Abs(c) < EPS)
                    sn = -1;
                else
                    sn = 0;
            }
            else
            {
                sn = 1;
                x1 = -c / b;
            }
        }
        else
        {
            double delta = b * b - 4 * a * c;

            if (delta < -EPS)
            {
                sn = 0;
            }
            else if (Math.Abs(delta) < EPS)
            {
                sn = 1;
                x1 = -b / (2 * a);
            }
            else
            {
                sn = 2;

                x1 = (-b - Math.Sqrt(delta)) / (2 * a);
                x2 = (-b + Math.Sqrt(delta)) / (2 * a);

                if (x1 > x2)
                {
                    double tmp = x1;
                    x1 = x2;
                    x2 = tmp;
                }
            }
        }

        return sn;
    }
}