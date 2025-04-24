# run this from PM console!
Scaffold-DbContext Name=ConnectionStrings:default MySql.EntityFrameworkCore -OutputDir Model -Tables Ingredients, RecipeImages, Recipes, Units, Users -Force -Context KochbuchContext
Pause
