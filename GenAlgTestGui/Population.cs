using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenAlgTest
{
    // TODO:
    // заменить NextGeneration на отдельные этапы
    // - генерация новых особей
    // - мутация (части из них)
    // - кросовер (части из них)
    // - селекция
    // - добавить банкинг и ограничение локальных минимумов

    interface IPopulation
    {
        Chromosome Mutate(Chromosome chromosome);
        Chromosome Crossover(Chromosome a, Chromosome b);
        void NextGeneration();
    }

    /// <summary>
    /// Популяция
    /// </summary>
    public class Population : List<Chromosome>
    {
        // Генератор случайных чисел
        private Random Rnd;
        // Размер популяции (ограничение для селекции
        public int Lenght { get; private set; }
        // Длина генетического кода (массива генов)
        private int GensCount;
        // Нормированная функция приспособленности особей
        public Func<double[], double> Fitness { get; private set; }
        // Радиус мутации (+- MumateRange для особи)
        private double MutateRange = 0.2;
        // Диапазон значений генов
        private Tuple<double,double> Range;
        // Возраст популяции
        public long Age { get; private set; }
        // Максимальный возраст особи, при котором используется банкование решения
        private int? MaxAgeToBank;
        // Банк решений
        public List<Chromosome> Bank = new List<Chromosome>();

        /// <summary>
        /// Создание популяции
        /// </summary>
        /// <param name="gensCount">Количество генов</param>
        /// <param name="fitnessFunction">Функция приспособленности</param>
        /// <param name="count">Размер особей оставляемых в популяции после селекции</param>
        /// <param name="mutateRange">Радиус мутации (+- к текущему значению гена)</param>
        /// <param name="maxAgeToBank">Максимальный возраст особи, при котором используется банкование решения. Null - не использовать банкование</param>
        /// <param name="range">Диапазон значений генов</param>
        public Population(int gensCount, Func<double[],double> fitnessFunction, int count = 5, double mutateRange = 0.2, int? maxAgeToBank = 30000, Tuple<double, double> range = null)
        {
            Rnd = new Random();
            GensCount = gensCount;
            Fitness = fitnessFunction;
            Lenght = count;
            MutateRange = mutateRange;
            MaxAgeToBank = maxAgeToBank;
            Range = range ?? new Tuple<double, double>(0,1);
            CreateRandomChromosomes(Lenght);
        }

        /// <summary>
        /// Создание N особей в популяции
        /// </summary>
        /// <param name="count">Количество особей в популяции</param>
        private void CreateRandomChromosomes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                double[] gens = new double[GensCount];
                for (int j = 0; j < gens.Length; j++)
                    gens[j] = Rnd.NextDouble() * (Range.Item2 - Range.Item1) + Range.Item1;
                Add(new Chromosome(gens));
            }
        }

        /// <summary>
        /// Сортировка популяции по убыванию функции приспособленности
        /// </summary>
        public new void Sort()
        {
            // TODO: сделать нормально
            var sorted = this.OrderByDescending(c => Fitness(c.Gens)).ToArray();
            this.Clear();
            this.AddRange(sorted);
        }

        /// <summary>
        /// Создание мутации хромосомы
        /// </summary>
        /// <param name="a">Исходная хромосома</param>
        /// <returns>Мутировавшая хромосома</returns>
        private Chromosome Mutate(Chromosome a)
        {
            var n = new Chromosome(GensCount);
            for (int i = 0; i < GensCount; i++)
                n.Gens[i] = a.Gens[i] + Rnd.NextDouble() * MutateRange * 2 - MutateRange;
            return n;
        }

        /// <summary>
        /// Скрещивание хромосом
        /// </summary>
        /// <param name="a">Первый родитель</param>
        /// <param name="b">Второй родитель</param>
        /// <returns>Потомок</returns>
        private Chromosome CrossOver(Chromosome a, Chromosome b)
        {
            var percent = new double[GensCount];
            for (int i = 0; i < GensCount; i++)
                percent[i] = Rnd.NextDouble();

            var n = new Chromosome(GensCount);
            for (int i = 0; i < GensCount; i++)
                n.Gens[i] = percent[i] * a.Gens[i] + (1 - percent[i]) * b.Gens[i];
            return n;
        }

        /// <summary>
        /// Переход к следующему поколению
        /// </summary>
        public void NextGeneration()
        {
            // Сохраняем текущую популяцию в массив
            var prevGen = this.ToArray();

            // Для каждой (переделать!) особи применяем мутацию и добавляем в популяцию
            foreach (var chr in prevGen)
                Add(Mutate(chr));

            // Скрещивание всех особей
            for (int i = 0; i < prevGen.Length; i++)
            {
                for (int j = 0; j < prevGen.Length; j++)
                {
                    if (i == j)
                        continue;

                    var child = CrossOver(prevGen[i], prevGen[j]);
                    if (child != null)
                        Add(child);
                }
            }
            
            // Увеличиваем возраст для каждой хромосомы
            this.ForEach(c => c.Age++);

            // Добавляем ещё случайных хромосом
            CreateRandomChromosomes(Lenght / 2);
            
            // Банкование
            if (MaxAgeToBank.HasValue && this.First().Age > MaxAgeToBank.Value)
            {
                Bank.Add(this.First());
                this.RemoveAll(c => true);
                CreateRandomChromosomes(Lenght);
            }

            // Сортируем по убыванию Fitness-функции
            Sort();

            // Селекция
            this.RemoveRange(Lenght, this.Count - Lenght);

            // Увеличиваем возраст популяции
            Age++;
        }
    }
}
