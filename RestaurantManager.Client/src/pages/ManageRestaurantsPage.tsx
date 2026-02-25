import { useMemo, useState } from "react";
import ManageRestaurantsHeader from "../components/restaurants/ManageRestaurantsHeader";
import ManageRestaurantsTable from "../components/restaurants/ManageRestaurantsTable";
import {
  X,
} from "lucide-react";
import type { Restaurant } from "../types/restaurants";

type RestaurantStatus = "Open" | "Closed";

const initialRestaurants: Restaurant[] = [
  { id: "r1", name: "Bella Italia", location: "Sofia", status: "Open", cuisine: "Italian", description: "", imgUrl: null, ownerId: null },
  { id: "r2", name: "Sushi World", location: "Plovdiv", status: "Closed", cuisine: "Japanese", imgUrl: null, ownerId: null, description: "" },
  { id: "r3", name: "Burger Palace", location: "Varna", status: "Open", cuisine: "American", imgUrl: null, ownerId: null, description: "" },
  { id: "r4", name: "Taco Fiesta", location: "Burgas", status: "Open", cuisine: "Mexican", imgUrl: null, ownerId: null, description: "" },
];

function classNames(...v: Array<string | undefined | false>) {
  return v.filter(Boolean).join(" ");
}

type RestaurantFormValues = Omit<Restaurant, "id">;

const emptyForm: RestaurantFormValues = {
  name: "",
  location: "",
  status: "Open",
  cuisine: "",
  description: "",
  imgUrl: null,
  ownerId: null
};

function ModalShell({
  title,
  children,
  onClose,
}: {
  title: string;
  children: React.ReactNode;
  onClose: () => void;
}) {
  return (
    <div className="fixed inset-0 z-50">
      <div className="absolute inset-0 bg-black/30" onClick={onClose} />
      <div className="absolute left-1/2 top-1/2 w-[92vw] max-w-lg -translate-x-1/2 -translate-y-1/2 rounded-xl border border-slate-200 bg-white shadow-xl">
        <div className="flex items-center justify-between border-b border-slate-200 px-5 py-4">
          <h3 className="text-sm font-semibold text-slate-900">{title}</h3>
          <button
            className="rounded-lg p-2 hover:bg-slate-100"
            aria-label="Close"
            onClick={onClose}
          >
            <X className="h-4 w-4 text-slate-700" />
          </button>
        </div>
        <div className="px-5 py-4">{children}</div>
      </div>
    </div>
  );
}

const RestaurantForm = ({
  initial,
  submitLabel,
  onSubmit,
  onCancel,
}: {
  initial: RestaurantFormValues;
  submitLabel: string;
  onSubmit: (values: RestaurantFormValues) => void;
  onCancel: () => void;
}) => {
  const [values, setValues] = useState<RestaurantFormValues>(initial);
  const [errors, setErrors] = useState<Record<string, string>>({});

  function validate(v: RestaurantFormValues) {
    const e: Record<string, string> = {};
    if (!v.name.trim()) e.name = "Name is required";
    if (!v.location.trim()) e.location = "Location is required";
    if (!v.cuisine.trim()) e.cuisine = "Cuisine is required";
    return e;
  }

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    const eMap = validate(values);
    setErrors(eMap);
    if (Object.keys(eMap).length > 0) return;
    onSubmit({
      name: values.name.trim(),
      location: values.location.trim(),
      cuisine: values.cuisine.trim(),
      status: values.status,
      description: "",
      imgUrl: null,
      ownerId: null
    });
  }

  return (
    <form className="space-y-4" onSubmit={handleSubmit}>
      <div>
        <label className="text-xs font-medium text-slate-700">Name</label>
        <input
          className={classNames(
            "mt-1 w-full rounded-lg border px-3 py-2 text-sm outline-none focus:border-slate-400",
            errors.name ? "border-rose-300" : "border-slate-200"
          )}
          value={values.name}
          onChange={(e) => setValues((p) => ({ ...p, name: e.target.value }))}
          placeholder="e.g. Bella Italia"
        />
        {errors.name && <div className="mt-1 text-xs text-rose-600">{errors.name}</div>}
      </div>

      <div>
        <label className="text-xs font-medium text-slate-700">Description</label>
        <input
          className={classNames(
            "mt-1 w-full rounded-lg border px-3 py-2 text-sm outline-none focus:border-slate-400",
            errors.name ? "border-rose-300" : "border-slate-200"
          )}
          value={values.description}
          aria-multiline
          onChange={(e) => setValues((p) => ({ ...p, description: e.target.value }))}
          placeholder="e.g. Bella Italia"
        />
        {errors.name && <div className="mt-1 text-xs text-rose-600">{errors.name}</div>}
      </div>

      <div>
        <label className="text-xs font-medium text-slate-700">Location</label>
        <input
          className={classNames(
            "mt-1 w-full rounded-lg border px-3 py-2 text-sm outline-none focus:border-slate-400",
            errors.location ? "border-rose-300" : "border-slate-200"
          )}
          value={values.location}
          onChange={(e) => setValues((p) => ({ ...p, location: e.target.value }))}
          placeholder="e.g. Sofia"
        />
        {errors.location && <div className="mt-1 text-xs text-rose-600">{errors.location}</div>}
      </div>

      <div>
        <label className="text-xs font-medium text-slate-700">Cuisine</label>
        <input
          className={classNames(
            "mt-1 w-full rounded-lg border px-3 py-2 text-sm outline-none focus:border-slate-400",
            errors.cuisine ? "border-rose-300" : "border-slate-200"
          )}
          value={values.cuisine}
          onChange={(e) => setValues((p) => ({ ...p, cuisine: e.target.value }))}
          placeholder="e.g. Italian"
        />
        {errors.cuisine && <div className="mt-1 text-xs text-rose-600">{errors.cuisine}</div>}
      </div>

      <div>
        <label className="text-xs font-medium text-slate-700">Status</label>
        <select
          className="mt-1 w-full rounded-lg border border-slate-200 px-3 py-2 text-sm outline-none focus:border-slate-400"
          value={values.status}
          onChange={(e) => setValues((p) => ({ ...p, status: e.target.value as RestaurantStatus }))}
        >
          <option value="Open">Open</option>
          <option value="Closed">Closed</option>
        </select>
      </div>

      <div className="flex justify-end gap-2 pt-2">
        <button
          type="button"
          onClick={onCancel}
          className="rounded-lg border border-slate-200 px-4 py-2 text-sm font-semibold text-slate-700 hover:bg-slate-50"
        >
          Cancel
        </button>
        <button
          type="submit"
          className="rounded-lg bg-slate-900 px-4 py-2 text-sm font-semibold text-white hover:bg-slate-800"
        >
          {submitLabel}
        </button>
      </div>
    </form>
  );
}

export default function ManageRestaurantsPage() {
  const [rows, setRows] = useState<Restaurant[]>(initialRestaurants);

  const [createOpen, setCreateOpen] = useState(false);
  const [editTarget, setEditTarget] = useState<Restaurant | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<Restaurant | null>(null);

  const subtitle = useMemo(
    () => `Manage restaurants in your portfolio. Create, edit, open menus, or remove old entries.`,
    []
  );

  function createRestaurant(values: RestaurantFormValues) {
    const newRestaurant: Restaurant = {
      id: crypto.randomUUID(),
      ...values,
    };
    setRows((prev) => [newRestaurant, ...prev]);
    setCreateOpen(false);
  }

  function updateRestaurant(id: string, values: RestaurantFormValues) {
    setRows((prev) => prev.map((r) => (r.id === id ? { ...r, ...values } : r)));
    setEditTarget(null);
  }

  function deleteRestaurant(id: string) {
    setRows((prev) => prev.filter((r) => r.id !== id));
    setDeleteTarget(null);
  }

  return (
    <div className="space-y-4">
      {/* Header area (matches your app's style) */}

      <ManageRestaurantsHeader
        subtitle={subtitle}
        setCreateOpen={setCreateOpen} />

      {/* Table card */}
      <ManageRestaurantsTable
        data={rows}
        setDeleteTarget={setDeleteTarget}
        setEditTarget={setEditTarget}
        setRows={setRows}
        classNames={classNames} />

      {/* Create modal */}
      {createOpen && (
        <ModalShell title="New Restaurant" onClose={() => setCreateOpen(false)}>
          <RestaurantForm
            initial={emptyForm}
            submitLabel="Create"
            onSubmit={createRestaurant}
            onCancel={() => setCreateOpen(false)}
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
