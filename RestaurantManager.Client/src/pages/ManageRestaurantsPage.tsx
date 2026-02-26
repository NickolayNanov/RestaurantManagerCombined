import { useEffect, useRef, useState } from "react";
import ManageRestaurantsHeader from "../components/restaurants/ManageRestaurantsHeader";
import ManageRestaurantsTable from "../components/restaurants/ManageRestaurantsTable";
import type { Restaurant, RestaurantFormValues, SingleRestaurantApiResponse } from "../types/restaurants";
import { apiFetch } from "../api/apiFetch";
import RestaurantForm from "../components/restaurants/RestaurantForm";
import ModalShell from "../components/modals/ModalShell";

const initialRestaurants: Restaurant[] = [
  { id: "r1", name: "Bella Italia", location: "Sofia", status: "Open", cuisine: "Italian", description: "", imgUrl: "", ownerId: null },
  { id: "r2", name: "Sushi World", location: "Plovdiv", status: "Closed", cuisine: "Japanese", imgUrl: "", ownerId: null, description: "" },
  { id: "r3", name: "Burger Palace", location: "Varna", status: "Open", cuisine: "American", imgUrl: "", ownerId: null, description: "" },
  { id: "r4", name: "Taco Fiesta", location: "Burgas", status: "Open", cuisine: "Mexican", imgUrl: "", ownerId: null, description: "" },
];

export const classNames = (...v: Array<string | undefined | false>) => {
  return v.filter(Boolean).join(" ");
}

const emptyForm: RestaurantFormValues = {
  name: "",
  location: "",
  status: "Open",
  cuisine: "",
  description: "",
  imgUrl: "",
};

const ManageRestaurantsPage = () => {
  const [rows, setRows] = useState<Restaurant[]>(initialRestaurants);

  const [createOpen, setCreateOpen] = useState(false);
  const [editTarget, setEditTarget] = useState<Restaurant | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<Restaurant | null>(null);

  const didInit = useRef(false);

  useEffect(() => {
    if (didInit.current) return;
    didInit.current = true;

    void fetchRestaurants();
  }, []);

  const createRestaurant = async (formData: RestaurantFormValues) => {
    const response = await apiFetch("api/restaurants", {
      method: "POST",
      body: JSON.stringify(formData)
    });

    if (response) {
      const newRestaurant: Restaurant = {
        id: response.id,
        name: response.name,
        description: response.description,
        status: response.status,
        cuisine: response.cuisine,
        location: response.location,
        imgUrl: response.imgUrl,
        ownerId: response.ownerId
      }

      setRows((prev) => [newRestaurant, ...prev]);
      setCreateOpen(false);
    }
  }

  const updateRestaurant = async (id: string, formData: RestaurantFormValues) => {
    await apiFetch("api/restaurants", {
      method: "PUT",
      body: JSON.stringify({ id, ...formData })
    });

    setRows((prev) => prev.map((r) => (r.id === id ? { ...r, ...formData } : r)));
    setEditTarget(null);
  }

  const deleteRestaurant = async (id: string) => {
    debugger
    await apiFetch(`api/restaurants/${id}`, {
      method: "DELETE"
    });
    setRows((prev) => prev.filter((r) => r.id !== id));
    setDeleteTarget(null);
  }

  const fetchRestaurants = async () => {
    const data = await apiFetch("api/restaurants", {
      method: "GET"
    });
    
    const restaurants = data.restaurants.map((r: SingleRestaurantApiResponse) => {
      return {
        id: r.id,
        name: r.name,
        description: r.description,
        status: r.status,
        cuisine: r.cuisine,
        location: r.location,
        imgUrl: r.imgUrl,
        ownerId: r.ownerId
      }
    });

    if (restaurants) {
      setRows(restaurants);
    }
  }

  return (
    <div className="space-y-4">
      {/* Header area (matches your app's style) */}

      <ManageRestaurantsHeader setCreateOpen={setCreateOpen} />

      {/* Table card */}
      <ManageRestaurantsTable
        data={rows}
        setDeleteTarget={setDeleteTarget}
        setEditTarget={setEditTarget}
        classNames={classNames} 
        fetchRestaurants={fetchRestaurants} />

      {/* Create modal */}
      {createOpen && (
        <ModalShell title="New Restaurant" onClose={() => setCreateOpen(false)}>
          <RestaurantForm
            initial={emptyForm}
            submitLabel="Create"
            onSubmit={createRestaurant}
            onCancel={() => setCreateOpen(false)}
            classNames={classNames}
          />
        </ModalShell>
      )}

      {/* Edit modal */}
      {editTarget && (
        <ModalShell title={`Edit: ${editTarget.name}`} onClose={() => setEditTarget(null)}>
          <RestaurantForm
            initial={{
              name: editTarget.name,
              location: editTarget.location,
              cuisine: editTarget.cuisine,
              status: editTarget.status,
              description: editTarget.description,
              imgUrl: editTarget.imgUrl,
              ownerId: editTarget.ownerId
            }}
            submitLabel="Save"
            onSubmit={(values) => updateRestaurant(editTarget.id, values)}
            onCancel={() => setEditTarget(null)}
            classNames={classNames}
          />
        </ModalShell>
      )}

      {/* Delete modal */}
      {deleteTarget && (
        <ModalShell title="Delete restaurant?" onClose={() => setDeleteTarget(null)}>
          <div className="space-y-4">
            <p className="text-sm text-slate-700">
              Are you sure you want to delete <span className="font-semibold">{deleteTarget.name}</span>?
              This will remove it from the list. (Dummy data only for now)
            </p>

            <div className="flex justify-end gap-2">
              <button
                onClick={() => setDeleteTarget(null)}
                className="rounded-lg border border-slate-200 px-4 py-2 text-sm font-semibold text-slate-700 hover:bg-slate-50"
              >
                Cancel
              </button>
              <button
                onClick={() => deleteRestaurant(deleteTarget.id)}
                className="rounded-lg bg-rose-600 px-4 py-2 text-sm font-semibold text-white hover:bg-rose-700"
              >
                Delete
              </button>
            </div>
          </div>
        </ModalShell>
      )}
    </div>
  );
}

export default ManageRestaurantsPage