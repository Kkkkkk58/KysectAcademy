import Server


def main():
    tc = 'Testing cookie'
    ts = 'Testing shop'

    workspace = Server.Server('test_shop.csv', 'test_prod.csv')
    workspace.add_shop(ts)
    workspace.add_prod(tc)
    workspace.prods_to_shop(tc, 5, 10, 50.0)
    # workspace.prods_to_shop(tc, 5, 10, 'wtf')
    workspace.find_cheapest('Chocolate Alenka')
    workspace.find_cheapest('Chocolate "Alenka"')
    workspace.find_cheapest('TV PHILIPS')
    workspace.find_cheapest('Testing cookie')
    workspace.find_cheapest('Cookie "Who was a good boy?"')
    workspace.what_can_buy(2, 10)
    workspace.what_can_buy(2, 100)
    workspace.what_can_buy(2, 10**9)
    print(workspace.buy_lot({'TV PHILIPS': 1, 'Chocolate "Alenka"': 2}, 2))
    print(workspace.buy_lot({'Testing cookie': 1, 'Chocolate "Alenka"': 2}, 2))
    print(workspace.buy_lot({'TV PHILIPS': 1, 'Chocolate "Alenka"': 2}, 3))
    workspace.cheapest_lot({'TV PHILIPS': 1, 'Chocolate "Alenka"': 50})
    workspace.cheapest_lot({'TV PHILIPS': 1, 'Testing cookie': 50})
    workspace.cheapest_lot({'Testing cookie': 50})


if __name__ == '__main__':
    main()
