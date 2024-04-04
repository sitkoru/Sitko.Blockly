using System.Text.Json;
using FluentAssertions;
using Sitko.EditorJS.Blocks;
using Sitko.EditorJS.Blocks.Paragraph;
using Sitko.EditorJS.Blocks.SimpleImage;
using Sitko.EditorJS.Data;

namespace Sitko.EditorJS.Tests;

[CollectionDefinition("Json", DisableParallelization = true)]
public class JsonTests
{
    [Fact]
    public void Deserialize()
    {
        ContentBlocksRegistry.Clear();
        var json = """
                   {
                     "time": 1712168519971,
                     "blocks": [
                       {
                         "id": "oJJ-PIRzzz",
                         "type": "paragraph",
                         "data": {
                           "text": "\u0412\u0430\u0443, \u0442\u0443\u0442 \u043C\u043E\u0436\u043D\u043E \u043F\u0438\u0441\u0430\u0442\u044C\u003Cbr\u003E"
                         }
                       },
                       {
                         "id": "2tsihegW-Y",
                         "type": "paragraph",
                         "data": {
                           "text": "\u0418 \u0442\u0443\u0442\u003Cbr\u003E"
                         }
                       },
                       {
                         "id": "Aqta7JFSeu",
                         "type": "paragraph",
                         "data": {
                           "text": "\u003Cb\u003E\u0418 \u0441\u0442\u0438\u043B\u0438 \u043A\u0430\u043A\u0438\u0435-\u0442\u043E\u003C/b\u003E\u003Cbr\u003E"
                         }
                       },
                       {
                         "id": "zuU72THimY",
                         "type": "image",
                         "data": {
                           "url": "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAoHCBYWFRgVFhYYGBgaGhoeHBwaGhweGhwZHBwaGh4aHBgeIS4lHB4rIRgcJzgmKy8xNTU1GiQ7QDszPy40NTEBDAwMEA8QHxISHzQrJSs0NDQ0NDQ0NDQ0NDQ0PTQ0NDQ0NDQ0NDQ0NDQ0NDQ0NDQ0NDQ0NDQ0NDQ0NDQ0NDQ0NP/AABEIAMIBAwMBIgACEQEDEQH/xAAcAAABBQEBAQAAAAAAAAAAAAAFAAIDBAYHAQj/xAA6EAACAQIEAwYEBQMFAAMBAAABAhEAAwQSITEFQVEGImFxgZETMqGxQlLB0fAHYnIUIzOS4UNTohf/xAAZAQADAQEBAAAAAAAAAAAAAAAAAQIDBAX/xAAnEQADAQADAQABAwMFAAAAAAAAAQIRAyExEkETMlEEYcEUIjNCof/aAAwDAQACEQMRAD8AK8WtIjhRmbYBmGXQ7ED8poFdZpmCAD9ennRWzezEzudp1gD\u002BfSocZdzsCdZ3gQJ2kj2rn1bpoh9virBfhBu63KO7J/EfGtBwlEe4oUaZFDjaSPmVeoMDzFZdcOFZtZAbw8YIFEOHYwqZiTr5aiJ86c1jE0dGweFQIuVREcxrEzBrPdpeFKoV17okhvXb9qL8Bx63LYH4lkEfWapdo8XmBRDMKc4gEQQIJ8Nd\u002BVaV5pK/gxaprHM6Uy9hj3hzOkc6LcAsM7uFUN3Cpndc2mYc5BrzjToxD6ZyAGA1WeZJ0IMjbxqPnrSt7wzbYFx\u002BGr3DrzWzrpoQfKmrmGzkz7AdKctxgZ0PnWbZRaGLBBJzZ9g2wy859NhpTzeCOGAMHLImQzDmR7mhq4kFupqdcRB1HdnXn51LGHuDOik51BzDQ6EjvaiPyxH1qlxSxDZlGQNIyzJAAEEkcjMwKrWr/fDqDkXUHlqDA6ctRUOJx/fQLMRrI\u002BYnc\u002BFIYSTFXUTKsjvaNyCEAFT4TBpiOS2Zy7LtI7pBHOI\u002BUVErvlEnTcT9JqfhuEFxnBeGykgE7\u002BQppNi8Fj/h51\u002BCM0LHeJHeMxG2oolwvE5yZXKwRQw6sJ1qqvDWCZnVguaIiNtd94O08qm4UBnYKSVVQNeuuoMCV0\u002BhqvnH2VL7Q/jGFFxGXwoH2duBEydDHtWrZZrLYvD/AArpA0VtvOp3DrxUFcRc9uZ/bqaG3TrtH\u002BZA/wDyNat20Jgx69KpPi0zlBBPWhsaSXSFB8/SvKbisWyiAJMelA7/ABG7sYUdQNqSWhT\u002BV2abs9pduDqu48D/AO1c4lhhOYVkuz14piEY31fNoymQdfPetrj0MaU6WCmvrsEvJU1d4HxEW370hW39Ovh5dKq3FMRVS5QhckqkdCOISA\u002BaQQII2g7eVCHxJt4lu4zlx3WGizA0J9KC4bijIihWj80zqRzozgOOgxnZSI3EyI6g9aapo4XOBvCllQZzJ3JPLwnn51JeuGO6JPnHrNVP9VbdCS6hWBGpHlTMKiIshpEfNIPiNqHdYJI9\u002BDd/\u002BwDwA2pVTu8YEmIpVnhp8V/BgGuBMqP8w3y67/8AlRm8paATEncQdNpnbrQ/EXGczAnnAifGKdYvKFLHVwRCkEggzPkf/K3UkssrjO9rJE79B6VaTFrvt9KDG90HvT0eRy8qbkRp8LxFkMqxHWDBI8xrUX\u002BsBJZzM7yx18zzoF8Q7E1C9wzrtzpY/Aw0Fx3WXR2EyCRpKnkSDrVcsTGYgR40Ow2LyyN9CRI/FyqB3djqQZ8vaKMfgwu95QcpOw9PDzqxheHreBZr6WlXctMnyG1ZtYQZn0A0A6n7xTg7M3eOsdNI8ByqplJg/Ag\u002BFVLndcOoO\u002Bo9fKraLOhZQNtafgeA33QOmVVOxY6n0ipG7NPHeuD2NNzL7Gt/gV5yqFdGOYnMG3BEbfrXtvAnOVDK5iQVmNpO8bfpQ2/wi4sw4Poafw\u002B\u002B9p0\u002BKoZNASu6j8w8RUOVnQfNL1BjGYZlJ0CqANyJEnXUb60U7IXz8VkCmBqddNYGaTz1OlT8WwVvIXDhkCzOpgFZBMcjG/jWUTEMrBkJXUNAaNRqKiHj0Vdo0/aTjhZjZUkKGIfk2UDbyJ8tKbw52DQVynIszzMn9CKzHEuIqbhcLqwGadjvJ8zRXs67kwxkSzARsTG557VdPvRz00aO4aFdoLGa3nG6waLOKhvICpB6RFZs757Rz9uKXnUkuwTYRzincPD6nKfY/epMJhwl90dC6bqAYifH1NGbKfgUEL0Jk7zqTvVVS8ImXpKcKWtho86q4WyiMWZA4Igg/vyo5hhKFfOhT3srQ2lZ\u002BGrX0C8ZhF/AuXvZpklgfAnlWws3A9tT1AoMGWNhFN4RxIZ2tnQjUDl5A0dsXypLV9YNUryb1ev6marX00Bpg10DcQ8Ltt4da0XBeFFrIuERpK67j\u002B4bcqz9xwCGIDAHUHYirWL7SFk\u002BHblNNQCIgcgav5bOO0tHWsRDasAAROg2EmCB7UTPaa1cb4Q7j8tO6x0AAP6GsN/rMzFRLGPQbanrVTDnOxgA6\u002BMjpEHernhfrMftJnRfiP8AkQ0qD2\u002BM31ABRGIGpbc\u002BdKj9Gjo/1PGZ5EGrHQabbA1VdIPSdp6VfslA0ssgESNdfDwNQcQdmYM2xGnkPtRJiyqqHlrT0EamnW1102ivStNsEj0W/wCa074JbbX96VoMZ02/m9SM0HQ\u002B3WkIrJZaSY2PuegqyLYADD18OteWrkZgDEjpUloqJD/L05k9KPWI8w2EVwb1wwqyEXrr0qLBobl51kfh9BUD4qSANulMwGOWzirdxzCEw/SBXR84tRH1vR134YRFWICqBVC93jP0pcU4l8WRYBYhVYlVY5QRpm6acqA2sXiFZS7dwn8nLzFc78OqHuIuYq3QfFJM0YxHFLcbqdY8Z6R18KzmOe83eCQsmND16mon015MUnuBxzgNYznTb\u002B5SZVT5En0im3gwJLQBuIEGqWGYtek93uAeZkk/TWimIGcZmY6DYRrH35U66ZysGuhjPErmidPA6jeNd6J8GxQF5AAVE\u002BWtU7mqzMCee23NR4VDhMQvxVZWzDMBJ3Inx50egvTpmIWKqu8CiuLQFAw8KDXrc86ivTs4a\u002BpM7j3VLwY8/wCfrV83hlGXT\u002Bdaz3aoMCuSTlOsV7wvH90A9KTl5poqX1hqMA4EzVHiWQmcwB8aYlguRLmOgqyMCi6xP1\u002B9IpsHoe7O4HOs/jMWUcOuhBFaDiWKCoQKxuLuZjB1/etOOezO666Oh8KxqXUDA\u002BlXXthtIiuc8Gxb227vy8x\u002B3vW6w7l1Dq0g0qn5YTX0irjsLlnmKyvFE\u002BEDlnM\u002B56L0HnW1vXAwj\u002BTQDi2DzqVjXl4eNXxVjMeeNWgNEyYYv\u002BK40TzyjSPU1b7KIA1y6w7ltZJO08hVTtDdGdLS/LbRV05mASferDn4eAVdmvXGJ6lBAHpvXVunntYB8TiGd2eT3iTvSqGlTwZtLGBLkKiMzkDNGus7\u002BA1G9Q43AMoCuII0iQTHpV/gjw93OSO5KhTGbXYn8uoNMawWYcwY/aJrkxmulTDYVVQMxMtIAA1nlB2PX0NPs2xJWAORJGin9/CtLgODuwHdhklhm5gz3Z5AxoPM1U7SvZw7gQ0k5nWREmDA9DE01LYnRBbwttM6HKykBs0kabjKoME\u002BYrLYnEorEKZHIka1LxDjCkHIsA9Kyt68ZNaKBfQXfiABgdamxl0zAgAfyaDYe07wQJ2k8oom8Sw5gn2rRSkTTYxTTsagOQxMMPbSoHflTxLoV6U/wJenVcNhwiFlUxcCMSvNlUqcw8o9qELgMplc7QSQuQzvMbQBVjsjxgXcMqkjOndI56c6IYi66ywAPQTFcdPGejEJrQDj\u002BDi2EvafGz/EJ5ZjuvlGk9daG4uyWkozkGdCNiYJ9aNcU4rKAFBm89Pc8qFYTFBRLwAeQ/Sj6YOFvZQxDBFTMIOeA39zAAae49au4dgzZWbI3eGWCW0H5Y50G7V3w1s5SPmXnB35Vd7J3Q1h7xzO6uEIBGzcz\u002BLUaTO81bX\u002B36OaklWBHE8KufDRyhUPoBu0\u002BPQedV04OyNmIzag/t9qL4jFMjATKggpMwV3IYczrHpVa5xA3MTbkd0MMvXcbnnsKzTYJadBx5hFU76aeQrMYzEGYHWi3FbpnflQVNSWO/3NKnrOvhn5kz2IfvsCd\u002BRih13Dgag\u002BlR8ZuMtx9wJMVTsYsg6npXTPGvk5L5mrYVw3EHQ6mRRYcYDDQEnagdpg45SPtRPA4Bbqlc2VxsSYB8z1rGpw6eO9QOxN0u3eYdIGtUsfg8mh09Oo28K01rhCIc128kL\u002BC33nPhQXtLiFYlgMo/mlOX3iHWYCEvhFJO528P5p7VpOxXFM\u002Ba0513XxHOsJfulj4VZ4XjDauK43U/TnWtRqOZcuVqOuMo6UNxFqiOEuh0V1MgqDUeITSubw7fUY3jfDSwLKNR9RVbtHeB\u002BEimVt21XwzRJ\u002BtafEWtxyrH8cwzI\u002BaO6foeldMXqw4eaMeg6aVQ0q17Oc6XwXAF8Qts7AtJB/CNwD5VtrnALZyLJCrOkCTPVok6xWS7K4lVvlnIHdYjnmJI0Xx1JjwrYYjj9lEZyT3dxznp51lKWGlejMbeTC2zdYlmAjVvm8ANgPKuUdq\u002BLNiGNwgLIAgbaVreJvfv4Z8TcOVC021iYQ7H251zvG3J0rScIaKDuadw\u002B2ruoYaE61FcWpuDvF1Z60DWGvGDA2URyigOPQrdJHyv7ZhrWtsMCKo8TiIyacuUGslTT7NnOpoytw0sNchvPT3qyeHOVZp1HKP1oeDOo3rVVplU4FOFY02LxE6N95rb2MU76AZgRIM6\u002BUVzXiTNOedQfpy\u002Blabs7xEOgUtBAmsOaP\u002Bx08F9fIcxdhte4w8SNKz2IQhtToOQ6\u002BdFMfiH2NxorP8RxEI7bwvXcnT71nK015HnaAvGcTncAHRRHrzqXgmNa1mInvRz6c6CWtSOpMe9bjAJbsShXOWVTr11DfpXWp6w4KrvSJOK3XIXRjO5EmPOtV2d4W73VZ1y97ujeFHXxrH27n\u002B8WAgHltAro3YJs7uxk5FAE9WP7KaKlZ4SqehDi3zkeFDvlUnpNX\u002BJNLmhuNaEauJ\u002Bnqz\u002B1HP\u002BI8TZ3KvqATHhE0LdopuMfvsejH70xnmu1eI8y/3MlS\u002BRsxohhOMMm\u002Bvif1oQFmkykUnKfoTdT2jQXe0KwQI9Fj60Cx2OZ94HlURXSkLXhSmJk0fNVLGyAmpLa04IKdVmLZt\u002BxHFZU2GO2q\u002BVap0kRXJuHYw2riuDsdR1FdWwt0OiuDIYA\u002B9cvLOPTv4L\u002Bpx/grXEmhHEsKHUqaO3hrVHEJUS8Zrc/SMHc4Y8nSaVbPIOle1v8AZy/pIscBwCPnLsFXuAMfmVjJJUc9FHvQ3iHF3dbeHYgqHzFvxMSYk9d96rpcYsU66yeoH2ohwrhttsSFuEN/to6KCRrJlT1Ige9RNYQ51mx49YzWMuYKoSAOQIG9cZxAhiJ29q7Rj7a3AqfhYAdeRM\u002BNck7Q4bJdZeY38\u002BtXFd4Fw0kwPlLNA3O1H\u002BH8JUETM7\u002BFALNwq4cbitnhySgIUSR1\u002BtHJTXhXFMv0tWSRoeVMfFAtDD2NVTfZdCdZ186n4fwe9clwhyfmOi\u002B5qFrNH16RY5xlJRimmp30rIY1GR2U6GdR9f1rofFsOmDWwGAc3s\u002BeR\u002BBQIVPyklpnfuisVxW4puhfnRhKMfmH9pO\u002BkVtMv05rpPpFO64YA8iI9qisMyNKmIqa/hNIUxrOvLlE9KOdjUwtx/gYpGDMwCOGIhj\u002BFvA8jVNExWdg0cRc/Nr6VHxi4fg\u002BLEe29dWvf08wx\u002BV3X/qf0r3/APn2FKFHLP0JgZT1Ee1Qp7NH/UL5aOEWZFbDs/ikdybssAjFRMd/QAEjl\u002B1RdteybYG4sHNaf5WjUEbo3jznmKCWWKwB0n61oY7q02nEERX7qRtPeB1iYkCtz2Ds5bD3fzt9EEb9JJrnWmQEDvbnz3rq3CbPwsFbXmUBPm2v61NPJKUp0kgbjDLTQrilyEPl\u002BlEsQ0saEcVPcby/SuM9NeHLsae\u002B3\u002BR\u002B9RokV7i27xPifvTEueNdqPMrstW2iiCXLRAzJQhnqVLmlMkv4kWvwSKqFxUJNKgD1mmmk013imO00AeLvXQOxPEMyG0T3l2/xrn6DWivBsZ8K8j8uY6g6VFz9Sa8VfNI6hcSdZqBhNJLkjQ6Gva4z0ioyDpSq3SoFiAt1ALpbQAqeWxpnEsKbiI1t5dUlWJgq6ics\u002B8VrV7LXSDmZJKgRJ5Rzj0oVjcEcBDG8rTBFvLJPXfSPE1tM1/BxVc\u002B6H7\u002BJK2jcClmW2rZR80sFkeYzH2rnfF7BdcxUjXfpM6E1Yx/bnEsGRMiKdCcoLEeZ0\u002BlZzF8UuuuV3ZhMwdp8hWi437pU/1PzLWbpDasqXAmR9K1yNlQAnXlWIR4NbXszxBLjZnWfhIWyxMkQFOviZ9KdTrRlPIlrNZwDsorqt2\u002BDqZVOo5ZvPp0oz2g/wCMWkAUFkWTooE7AftQLDcYZbYYs7OQzM867d1Qp038Kx\u002BL4y91/wDcdpAgDkBzgDbarUYY1boJf1ZxSs\u002BGyMDlFwEjxy9OWlc5x5PdI2BBFHOJpIQMc3eMGeRymhWJtyGXodKrMEmWHfMsSQG1BB\u002BnlTUJAVgYzd0kcnQ5kb7j1rzAqWt5ea7U/DpJKHTPGQ7Q41GvQ6j1pMDs/Y/tKuLtwe7eQAOpO/LOvUH6UUxmJZHBMFCPUGdfpXCBiHsut1CysDM6gq4OoPhzjxrrXZ7tNax1sqe5dESPE6Ar1EnUUIzuXmot9reDrjcOUUgsveQ/3AGB67Vwm9bKPlIKkCCOYZTBB8f3ruGJvXLM5BDfKM3y94aNp0JH1rl/anDubxuMpzkjOYAGfkRGhBjceFNoOKtXZbwaZ2RBrmZR7xrXVuK3AoAGyiPsP0rmXY3DFsSh/IM09OlbzGX8361jyv8AB2cE69KjUJ4oe63rRO62lCsfqrDwNczO/OjmeLTVgd5P3qjm1ir/ABH/AJG8zVJ1rtXh5jWNjwaerxUCmpRTET17TLe1PiaCSO5TApqyluKtWbA33nrRow32S7GvilN1nVLYMTEux8BsB40U4twGxhmGQEmR3mgn9hW27P2Bbw1pAIAUH1IrJduMWiNmcwZ0UfMSJ2H1muBctXyOV4dEypWss4K5IirdYfhHaFnvBSAqnbrPia2SXaqpc\u002BnTxWqQ\u002BaVNpVB04dJmuH9pMe1\u002B/cdiYLEKOiAkKP51rsfE8SEtu06hGIHpXDsS8616EnhsE3jE1Xd6s4gb\u002BdUrtN\u002BjQviUY7NOfjgDmjgjqMpMH1AoFNaPsQim\u002B7N\u002BG1cI8yMo\u002B5oBroPrdIBHI0E4vaghhuPrRhljQ0P4qJEdR9aZnPoLs4jOondDoPMR9xVfEWzr4mqqX8j6z0PQjrRLEXABBGv09/5vS/BfgzhizoN6s4vAGCf5NVeCcS\u002BDdDrEidCJHSiV/HveZ2gS2pMQB5CmhP0psS6AvyORjzBglG8dARNLhGKOHuy22qtpMZhAcAcxMgivM2QlmJKN3XHPLMhl6FTB9xzpuMsx3DGbTK3JgdQAehGo8dKRR2K0FxGERwfiiMwY6FhPeDAbHeRFZbtlggFXKuRdDlgwI17v7eNL\u002BlHEV/3MOxOb5lBOhUaEBeo5npRbtgwKCyDJDaaakmdPLlTTMGsoo9jcLlR7hGrGAf7Rv5a0UuNqafhrQSyEHhPnzqteeK5OWtZ6vBOIjvPOnSht/UGrV1oFVXaKwZ0nOeMpluuPGf1qkdaJdoli6xHOhStXfH7UeZf7meMIp6Nyry6NKgtNFMk3nY/s0mJR2Z1kaKonunx9xQXiODW07Ir58u7RAnoAai4V2gvYdSlpgmYyTEkyIiTsKZdxjXGzuZY7mslNfTbfQ21gxGmrlm4qlWYSFIMdfCqaAzTnEGKsk1WN7YYl0hHFpAIAtgAxtqxBJ9IrC8QvMzFnZmbqTJ9zRgrCedLD9nL905ihRPzOCJ/xG5NSlE/2GvpgPCXCCGB1Brp3C8WLltWBB0rJ8V4Uli0QoltJY7nUewqbsnjYJQny86m8qdRtxV812bPNXlMzUq5Tv00XajGOlxk/C9sRMx\u002BINB9R9K5ffWCfOuy4HHYfG2g6hXHNT8yk8iNx\u002Btc37Y8NFnEFV\u002BVhmHhJOk16Enjtrwyl5IqlcSid62TSHCbjalSvSdPpQwXYCdSDWn7GYVyXePwhR03k6\u002BFNwXZ0u/faEGpjc\u002BFa20Vt22CKAFgDw3oTW4U08YPvCGPmaG45tdtCKvk0J4oSNtx9qoyQB4xh/wAS7Vc4TfDpB1KaHxHI1WW/mlWpnDj8O8AflcR59KW9mmdE9\u002BzkeIosj91WPl6GN/v6VU4rb1Vhzq1lOQHkaaEyK9bkEGqmAuh1/wBM5AdJCSYDLubebqDqvtVpbs6H36x/5QTiloqwdf4aKBfwFMFjnsXluJpdtmYIIzqJkEDWYkMPWuhYa6cTet3GAgrn0nL10nx3rm15zdtLdBh0gNG\u002BnyOPsfIV0LsKxOHLt/cB4Axp7k\u002B9TTxDU6w7fckmqV9tasO3OqT8/WuGnrPU45xEF56q3j3TUpqviW7sVHrNDEdoh/uHyoG7RR/tF848qz\u002BI3rvj9qPM5P3MsDaoLlszMafzWprT1efDhkDc4qiAWpjepUc0zJG/jUtvlQBdt3NK1fYrgKYlme9mKKPlUwS3ielZCK6r2CVbeFzuyoGMyzKo59TWH9RTmG59HxpN9mgwvCbFsAJZRY55ZP8A2Mmq/G8PmUGPD9qhx/anCWgS15XP5bffM9JGg9a512l7aXsQSiE2rfIKe\u002BfFnH2FcXFxcl19P/06KqUsPO1uIthTbzy8iVXWPBjy8qzGFvFGBB1BmoXMmvK9KZ\u002BVhg616dAwvGAUU6bdaVYhbxHM0qj9I0/VL/BuOXcM/wAS02VtjzBHRhzFGcd2lfFnPcVAVEdwEA\u002BJknXSsca9DmMo2NbGDR0fs3YRlLwCauY3DktO01kOyWPa0xVvlbat3ehlkVhbaemsrwGtbIEA15ni2wPMjf61M6xvVRkzoWk6E\u002B0Cjje0VyylJBmHWh2N1\u002BtWShXnv/P1qni21roOVLEZ/H28rhhpNR4lsyAjdTPpRHFAMCKE2W5HypFI0buHQdCAR5kVHaukoyncbVX4ZczWgOaGB5TpXivqfGjQGi546ztSxaFkMgae9ORjsKvWrGRGc6tHOgAVwO6IK7jUMDzB3H86V0jskQuHyA829QTpNcw4U8OR4mtpwDGZCQToT96i03PRfG0q7NaTMiqzmJqW1ybr9aiv6TFcR6iKVU8S3896ttVDEsAJ5b\u002BlTK7FXhk\u002B0b98DwoDcWTV3iN/Pcdup08ht9KgjSvQlYkjzae02MXSp7eJYCJqClTJHMZ20pttqciU070AWkeane8xABJIGwJJA8hyqmrc6mVpFAu/wPd6rsZqYioDQCIiKeq06lQMVKlSoA8ZPCvAtdouf0xw5Ol26P8Aqf0plr\u002Bl1gHvXbjDoAo\u002BtL6A5Vhj3RWt4JxYFMrcq97dcCw\u002BFa3bshsxEtmM93YfWsbfulAIMVNSqQ5rGbjH478u0VT4NjMyupBIzRpykdKyycRbLEz51ouxNwF7gPRT9Tt40RGMfLeyPxtzK0a6a\u002Bn8\u002B1VnWasY8M7s0TqdqrK2sE1sYr\u002B4FvsVciKHYzumRzo7xiwdDQDENpFSUi7wO58y9RV901ignDXh6OPrBFLQZJhretXLrSj\u002BVQ2enWvce4W0390\u002B2xqs6FpncAf9wf5GtHnI2rM4Md/1mtG21JeMH0zW8Dxudcu7DaavXm3FYvh\u002BNNtp5Vp7OKDiedcfJOM9Dg5PqcfozEvArNdocblXIDqdT4CjvFMcLSFjvsPOuf4m6XYsTJNHDO9sfPyZPyiud6ks6nwrzKVbKQQSAfQiR7jWn211rrOAhca15T741pgFAydBrUF7Q1atLVbFLqPOgD1KtAVUQ1KDQInqNwBrFIPT6BpdDGTpXmQ1LUTqZoJQylU80qB6fTtKlSqBnLf6nWIxCP8AmQD2J/euacRGtdK/qTj0uXlRGBNsFWjkxMx9K5pj2lqv8Er0rpRvsrigmIUEwHBX6T\u002BlBRUmGuZbiN0Yft\u002BtMbWm6xoyEgnnvQR3E7irWd7iQTopIHjQnE2WXcU9ISJcfigyhQZigOIXerjsKrYl0y7manSkQ4D5wJ3286OgMPmG3jWaBjUVoMPeNxVgTyJ8aaBl3DGYpnHXhcg5D/2rWETKQPOh3G0IYnkabEvQbgF71aFUkehoBgz3q0VvY\u002BX60SKvSmNZqNMY6bEx0qd\u002B7Hiar49NCRrpMeP8FKp6KmselfHYpn\u002BYkjkOk0R7N9nmvuGfu2xqSQZbwA5zWn7Jdj7eItpfdwUbXLz0MEGt0uCt2SCkGIhTqI2gdNppTKXRHJys4z23sZMUWgAFbZUDbIFCiP8AqaFW9TpzroX9Q\u002BEvfYYhFnIpDhR\u002BHkR1iNfOud4RQGNN\u002BjilSQ3EDWo0FOxDSa9tDakWWrS6VUxI2q\u002BBVXFJzpICsnKpaiUVIKYz0GpEYzUYr0igCelUdqpKCTzIKVe0qAPpym3Nj5GlSqBnBMaZe4TqTcM\u002BOprO4v5v50pUq0ZKIqY24pUqRZtMN/wer/pQ7E/IfMfavaVUvDP8gLG7GhlKlUloVHeBfKf8j9qVKheirwO2Pw\u002BbfaqHHth6UqVUxIEYT5q0uF5\u002BQ\u002B4pUqF4D9KeI5edRYjY\u002BX70qVDEja/05Y/6M6//ACN9lrS3flPp96VKrnw5b/5Cbh\u002BoAOoKPIOs6Nv1rnf9QMJbS8MiIn\u002BKgcz0pUqivTXi/wAmHvb1Ph9/alSqTo/BcWq2K2pUqBFNN6lpUqCj1NxT7te0qBfk8tVJSpUCYqVKlQB//9k=",
                           "caption": "\u0418 \u041A\u0410\u0420\u0422\u0418\u041D\u041A\u0418\u003Cbr\u003E",
                           "withBorder": false,
                           "withBackground": false,
                           "stretched": true
                         }
                       },
                       {
                         "id": "Hgq37g4Gh_",
                         "type": "paragraph",
                         "data": {
                           "text": "OMG"
                         }
                       },
                       {
                         "id": "OXV5zEDJ0K",
                         "type": "paragraph",
                         "data": {
                           "text": "\u0418 \u0432\u0441\u0451 \u044D\u0442\u043E JSON\u043E\u043C \u0443\u043B\u0435\u0442\u0430\u0435\u0442 \u0432 Blazor\u003Cbr\u003E"
                         }
                       }
                     ],
                     "version": "2.29.1"
                   }
                   """;

        ContentBlocksRegistry.Register<ParagraphBlock, ParagraphBlockOptions>();
        ContentBlocksRegistry.Register<SimpleImageBlock, SimpleImageBlockOptions>();
        var data = JsonSerializer.Deserialize<EditorJSData>(json);
        data.Should().NotBeNull();
        data!.Time.Should().Be(1712168519971);
        data.Version.Should().Be("2.29.1");
        data.Blocks.Should().NotBeEmpty();

        var textBlock = data.Blocks.OfType<ParagraphBlock>().FirstOrDefault(b => b.Id == "oJJ-PIRzzz");
        textBlock.Should().NotBeNull();
        textBlock!.Data.Text.Should().Be("Вау, тут можно писать<br>");

        var imageBlock = data.Blocks.OfType<SimpleImageBlock>().FirstOrDefault(b => b.Id == "zuU72THimY");
        imageBlock.Should().NotBeNull();
        imageBlock!.Data.Stretched.Should().BeTrue();
    }

    [Fact]
    public void Serialize()
    {
        ContentBlocksRegistry.Clear();
        ContentBlocksRegistry.Register<ParagraphBlock, ParagraphBlockOptions>();
        ContentBlocksRegistry.Register<SimpleImageBlock, SimpleImageBlockOptions>();
        var data = new EditorJSData
        {
            Time = 1,
            Version = "something",
            Blocks =
            [
                new ParagraphBlock { Id = "foo", Data = new ParagraphBlockData { Text = "some text" } },
                new SimpleImageBlock { Id = "bar", Data = new SimpleImageBlockData { Url = "some url" } }
            ]
        };

        var json = JsonSerializer.Serialize(data);
        var data2 = JsonSerializer.Deserialize<EditorJSData>(json);
        data2.Should().Be(data);
    }
}
