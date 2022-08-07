using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
<<<<<<< Updated upstream
        {
            Console.WriteLine("Hello World!");
=======
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);


            //Вывод всех товаров на складе с их остатком

            Cart cart = shop.Cart();

            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине


            Console.WriteLine(cart.Order());

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары

        }
    }

    class Good
    {
        private string _name;

        public Good(string name)
        {
            _name = name;
        }
    }

    class Cell
    {
        private int _reserevedGoodsCount;
        private int _lastReserevedGoodsCount;

        public Good Good { get; private set; }
        public int Count { get; private set; }

        public static Cell IsCellListHaveNeededCell(List<Cell> cells, Good good)
        {
            var neededCell = cells.FirstOrDefault(cell => cell.Good == good);

            if (neededCell != null)
            {
                return neededCell;
            }

            return null;
        }

        public Cell(Good good, int count)
        {
            Good = good;
            Count = count;
        }

        public void AddGoods(Good good, int count)
        {
            if (Good == good)
            {
                Count += count;
            }
        }

        public void ReserevGoods(Good good, int count)
        {
            if (good == Good && count > 0 && Count - count - _reserevedGoodsCount >= 0)
                _lastReserevedGoodsCount = count;
            else
                _lastReserevedGoodsCount = 0;
        }

        public int AddToCart()
        {
            _reserevedGoodsCount -= _lastReserevedGoodsCount;
            var temporaryLastReserve = _lastReserevedGoodsCount;
            _lastReserevedGoodsCount = 0;
            return temporaryLastReserve;
        }

        public bool Buy()
        {
            if (Count - _reserevedGoodsCount >= 0)
            {
                Count -= _reserevedGoodsCount;
                _reserevedGoodsCount = 0;
                _lastReserevedGoodsCount = 0;
                return true;
            }

            return false;
        }
    }

    class Warehouse
    {
        private List<Cell> _cells;
        private Cell _reserevedCell;
        private List<Cell> _reserevedCells;

        public Warehouse()
        {
            _cells = new List<Cell>();
            _reserevedCells = new List<Cell>();
        }

        public void Delive(Good good, int count)
        {
            if (count > 0)
            {
                var neededGood = Cell.IsCellListHaveNeededCell(_cells, good);

                if (neededGood != null)
                {
                    neededGood.AddGoods(good, count);
                }
                else
                {
                    Cell newCell = new Cell(good, count);
                    _cells.Add(newCell);
                }
            }
        }

        public bool IsEnoughtGoods(Good good, int count)
        {
            if (count > 0)
            {
                var neededGood = _cells.FirstOrDefault(cell => cell.Good == good);

                if (neededGood != null)
                {
                    if (neededGood.Count >= count)
                    {
                        neededGood.ReserevGoods(good, count);
                        _reserevedCell = neededGood;
                        return true;
                    }
                }
            }

            _reserevedCell = null;
            return false;
        }

        public int AddToCart(Good good)
        {
            var reserevedCell = Cell.IsCellListHaveNeededCell(_reserevedCells, good);

            if (reserevedCell == null)
                _reserevedCells.Add(_reserevedCell);

            var buyingGoodsCount = _reserevedCell.AddToCart();
            _reserevedCell = null;
            return buyingGoodsCount;
        }

        public bool Buy()
        {
            var isEverethingAllRight = true;

            foreach (var reserevedCell in _reserevedCells)
            {
                if (reserevedCell.Buy() == false)
                    isEverethingAllRight = false;
            }

            if (isEverethingAllRight)
            {
                _reserevedCells = new List<Cell>();
                return true;
            }

            return false;
        }
    }

    class Shop
    {
        private Warehouse _warehouse;
        private List<Cart> _carts;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
            _carts = new List<Cart>();
        }

        public Cart Cart()
        {
            var newCart = new Cart(this);
            _carts.Add(newCart);
            return newCart;
        }

        public bool IsEnoughtGoods(Good good, int count)
        {
            if (_warehouse.IsEnoughtGoods(good, count))
                return true;

            return false;
        }

        public int AddToCart(Good good)
        {
            return _warehouse.AddToCart(good);
        }

        public bool Buy(Cart cart)
        {
            if (_warehouse.Buy())
            {
                _carts.Remove(cart);
                return true;
            }

            return false;
        }
    }

    class Cart
    {
        private List<Cell> _cells;
        private Shop _shop;

        public Cart(Shop shop)
        {
            _cells = new List<Cell>();
            _shop = shop;
        }

        public void Add(Good good, int count)
        {
            if (count > 0)
            {
                if (_shop.IsEnoughtGoods(good, count))
                {
                    var neededGood = Cell.IsCellListHaveNeededCell(_cells, good);

                    if (neededGood != null)
                    {
                        neededGood.AddGoods(good, _shop.AddToCart(good));
                    }
                    else
                    {
                        Cell newCell = new Cell(good, _shop.AddToCart(good));
                        _cells.Add(newCell);
                    }
                }
            }
        }

        public bool Order()
        {
            if (_shop.Buy(this))
            {
                _cells = new List<Cell>();
                return true;
            }

            return false;
>>>>>>> Stashed changes
        }
    }
}
