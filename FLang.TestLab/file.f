include <file.f>

args => () { control<Cases> }

<Cases> -> {
    "z" => Call Another function 1
    "x" => Call Another function
    "w" => Call Another function
    "y" => Call Another function
    "n" => Call Another function
}

gl fn Name(in int name1, in str name2, out type x) {
    
    return x
}