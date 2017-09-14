# Binary-Tree-Printer

[![pipeline status](https://gitlab.com/dragon1672/Binary-Tree-Printer/badges/master/pipeline.svg)](https://gitlab.com/dragon1672/Binary-Tree-Printer/commits/master)

[![coverage report](https://gitlab.com/dragon1672/Binary-Tree-Printer/badges/master/coverage.svg)](https://gitlab.com/dragon1672/Binary-Tree-Printer/commits/master)

Creates a prudy display of a binary tree

Very fast run time since it uses magical math for calculating tree size. 


Example Trees:
--------------

### Balanced ###
             /---  0  ---\             
          /  1  \     /  2  \          
          3     4     5     6          

### UnBalanced ###
               /---0                   
             /-1-\                     
            /2   3                     
            4                          

                               /------------------------------ 2 ------------------------------\                               
               /-------------- 7 --------------\                                               1                               
       /------26 ------\                       3                                                                               
      90 --\          25 --\                                                                                                   
          36              19 \                                                                                                 
                            17                                                                                                 

### Huffman Tree ###
                               /----------------------------nul(13)----------------------------\                               
               /------------nul(5) ------------\                               /------------nul(8) ------------\               
             67(2)                     /----nul(3) ----\                     45(4)                     /----nul(4) ----\       
                                     34(1)         /nul(2) \                                         23(2)         /nul(2) \   
                                                 78(1)   89(1)                                                   12(1)   56(1) 
### Binary Heap Tree ###
           /------ 1 ------\           
       /-- 2 --\       /-- 3 --\       
      17      26      19     / 7 \     
                            25  90     
