.# Eigenvalues
Eigenvalues computation

The main goal of this project is to deduce the dynamic stability information for the first order differential system.
During this process we'll use:

1. Runge–Kutta method to determinate a equilibrium states through approximation of solutions of ordinary differential equations in infinite time period. All solution should stabilized at some points eventually (or will take a place at some infinite points). I'm going to consider just stabilized solutions as equilibrium states.

2. We have deduced asymptotic solutions which can be used to determine the asymptotic behavior of solutions to an ODE. So i'm going to compute an approximate equation solutions just to compare them with solutions that i'd got in previous step.

3. Probably i'm going to consider Floquet multipliers for random A and B parameters just in case. So the program must determinate values of this parameters in case where Floquet multipliers is less then 1.
