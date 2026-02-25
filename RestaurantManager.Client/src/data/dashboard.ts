import type { Restaurant } from "../types/restaurants";

export const dashboardKpis = {
  totalRestaurants: 12,
  overallRating: 4.8,
  monthlyRevenue: 25860,
  totalReviews: 1330,
};

export const restaurants: Restaurant[] = [
  { id: "r1", name: "Bella Italia", status: "Open", cuisine: "Italian", location: "Sofia", rating: 4.7 },
  { id: "r2", name: "Sushi World", status: "Closed", cuisine: "Japanese", location: "Plovdiv", rating: 4.6 },
  { id: "r3", name: "Burger Palace", status: "Open", cuisine: "American", location: "Varna", rating: 4.5 },
  { id: "r4", name: "Taco Fiesta", status: "Open", cuisine: "Mexican", location: "Burgas", rating: 4.4 },
];

export const analytics = [
  { month: "Jan", revenue: 12000, rating: 4.6 },
  { month: "Feb", revenue: 18000, rating: 4.7 },
  { month: "Mar", revenue: 15500, rating: 4.7 },
  { month: "Apr", revenue: 16500, rating: 4.8 },
  { month: "May", revenue: 23500, rating: 4.8 },
  { month: "Jun", revenue: 25860, rating: 4.8 },
];

export const feedback = {
  fiveStar: 79,
  fourStar: 16,
  threeStar: 3,
  twoStar: 1,
  oneStar: 1,
};

export const topDishes = [
  { id: "d1", name: "Margherita Pizza", restaurant: "Bella Italia" },
  { id: "d2", name: "Spicy Tuna Roll", restaurant: "Sushi World" },
  { id: "d3", name: "Cheeseburger", restaurant: "Burger Palace" },
  { id: "d4", name: "Chicken Tacos", restaurant: "Taco Fiesta" },
];