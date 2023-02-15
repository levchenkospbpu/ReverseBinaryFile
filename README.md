A console application that receives a binary file of arbitrary size as input and, as a result, creates a new file in which the contents of the input file are written in reverse order (byte by byte). The names of the input and output files must be passed through command line options:
program.exe input.dat output.dat
For example, if the input file is input.dat which contains the bytes {0x0A, 0x00, 0x20, 0xFF}, then the output will be the file output.dat which contains the bytes {0xFF, 0x20, 0x00, 0x0A}.

Time complexity is O(n)
Space complexity is O(1)
