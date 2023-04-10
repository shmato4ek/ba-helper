using System;

public class Analyzer
{ // Перелік станів автомата
    private enum States { S, A, B, C, D, E, F, G, H, I };
    //public static bool Val(string Str /* Початковий рядок */,
    //out double Value /* Значення числа */)
    static int Position = 0; // Поточний номер позиції у рядку
    static short Sign = 1; // Знак числа
    static public short Value = 0; // Значення числа
    static double Frac = 0.1; // Значення порядку дробової частини числа
    static States State = States.S;
    public static string Val(string Str, double Value)
    {
        while ((State != States.F) && (State != States.E) && Str.Length > Position)
        {
            char Symbol = Str[Position]; // Аналізований символ
            switch (State)
            {
                case States.S:
                    {
                        switch (Symbol)
                        {
                            case '-': { State = States.A; Sign = -1; break; }
                            case '+': { State = States.A; break; }
                            case '0': { State = States.B; break; }
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                {
                                    State = States.C;
                                    Value = Value * 10.0 + (Symbol - 48);
                                    break;
                                }
                            default:
                                {
                                    // Помилка! Очікується знак чи цифра.
                                    State = States.E;
                                    break;
                                }
                        }
                        break;
                    }
                case States.A:
                    {
                        switch (Symbol)
                        {
                            case '0': { State = States.G; break; }
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                {
                                    State = States.C;
                                    Value = Value * 10 + (Symbol - 48);
                                    break;
                                }
                            default:
                                {
                                    // Помилка у цілій частині числа!
                                    State = States.E;
                                    break;
                                }
                        }
                        break;
                    }
                case States.B:
                    {
                        switch (Symbol)
                        {
                            case '.': { State = States.D; break; }
                            case ';': { State = States.F; break; }
                            default:
                                {
                                    // Помилка! Очікується "." або ";"
                                    State = States.E;
                                    break;
                                }
                        }
                        break;
                    }
                case States.C:
                    {
                        switch (Symbol)
                        {
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                {
                                    State = States.H;
                                    Value = Value * 10 + (Symbol - 48);
                                    break;
                                }
                            case '.': { State = States.D; break; }
                            default:
                                {
                                    // Помилка! Очікується цифра або "."
                                    State = States.E;
                                    break;
                                }
                        }
                        break;
                    }
                case States.D:
                    {
                        if (char.IsDigit(Symbol))
                        {
                            State = States.I;
                            Value = Value + Frac * (Symbol - 48);
                            Frac /= 10;
                        }
                        else
                        {
                            // Помилка!
                            State = States.E;
                        }
                        break;
                    }
                case States.G:
                    {
                        if (Symbol == '.') { State = States.D; }
                        else
                        {
                            // Помилка! Очікується "."
                            State = States.E;
                        }
                        break;
                    }
                case States.H:
                    {
                        switch (Symbol)
                        {
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                {
                                    State = States.H;
                                    Value = Value * 10 + (Symbol - 48);
                                    break;
                                }
                            case '.': { State = States.D; break; }
                            default:
                                {
                                    // Помилка! Очікується "." чи цифра.
                                    State = States.E;
                                    break;
                                }
                        }
                        break;
                    }
                case States.I:
                    {
                        switch (Symbol)
                        {
                            case '0':
                            case '1':
                            case '2':
                            case '3':
                            case '4':
                            case '5':
                            case '6':
                            case '7':
                            case '8':
                            case '9':
                                {
                                    State = States.I;
                                    Value = Value + Frac * (Symbol - 48);
                                    Frac /= 10;
                                    break;
                                }
                            case ';': { State = States.F; break; }
                            default:
                                {
                                    // Помилка!
                                    State = States.E;
                                    break;
                                }
                        }
                        break;
                    }
            }
            Position++;
        }
        Value *= Sign;
        return State.ToString();
    }
}

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Analyzer.Val("val", 10).ToString());
        Console.ReadLine();
    }
}