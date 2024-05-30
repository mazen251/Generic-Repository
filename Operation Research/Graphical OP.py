from ortools.linear_solver import pywraplp

def LinearProgrammingExample():
    """Linear programming sample."""
    # Instantiate a Glop solver, naming it LinearExample.
    solver = pywraplp.Solver.CreateSolver('GLOP')
    if not solver:
        return

    # Create the two variables and let them take on any non-negative value.
    x = solver.NumVar(0, solver.infinity(), 'x')
    y = solver.NumVar(0, solver.infinity(), 'y')

    x1_coefficient = float(input("Please enter the coefficient of x1 of the objective function: "))
    x2_coefficient = float(input("Please enter the coefficient of x2 in the objective function: "))

    choice = int(input("Please enter the number of constraints: "))

    for i in range(choice):
        xcoefficient = float(input("Please enter x coefficient of the constraint: "))
        ycoefficient = float(input("Please enter y coefficient of the constraint: "))
        choice2 = int(input("Please enter 1-for bigger than or equal RHS 2-for smaller than or equal RHS: "))
        RHS = float(input("Please enter the RHS of the constraint: "))

        if i == choice - 1:
            choice3 = int(input("Please enter 1-for Maximization 2-for Minimization: "))
        if choice2 == 1:
            solver.Add(xcoefficient * x + ycoefficient * y <= RHS)
        if choice2 == 2:
            solver.Add(xcoefficient * x + ycoefficient * y >= RHS)

    print('Number of constraints =', solver.NumConstraints())

    if choice3 == 1:
        solver.Maximize(x1_coefficient * x + x2_coefficient * y)

    if choice3 == 2:
        solver.Minimize(x1_coefficient * x + x2_coefficient * y)

    # Solve the system.
    status = solver.Solve()

    if status == pywraplp.Solver.OPTIMAL:
        print('Solution:')
        print('Objective value =', solver.Objective().Value())
        print('x =', x.solution_value())
        print('y =', y.solution_value())
    else:
        print('The problem does not have an optimal solution.')


LinearProgrammingExample()
