# FLang


```rust
// Including files like this :
include <file.f>

// Set a Controller for Args : (like here)
args => () { control<Cases> }

// Define Controller and possible args: 
<Cases> -> {
    "-w" => Call Function 
    "-c" => Call Another Function
    "-n" => Call Another Function
    "-p" => Call Another Function
    "-h" => Call Another Function
}

gl fn GetFullName(in string name, in string family, out string fullName) {
  return name + family
}

pr fn DoSomething(out void) {
    Write("Some text");
}
```
