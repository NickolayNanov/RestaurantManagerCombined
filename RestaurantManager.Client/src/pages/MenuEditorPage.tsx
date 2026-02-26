import { Link, useParams } from "react-router-dom";

const MenuEditorPage = ({ }) => {
    const { restaurantId, menuId } = useParams();

    return (
        <div className="space-y-4">
            <div className="flex items-center justify-between">
                <div>
                    <h2 className="text-xl font-semibold text-slate-900">Menu Editor</h2>
                    <p className="mt-1 text-sm text-slate-600">
                        Restaurant: <span className="font-semibold">{restaurantId}</span> • Menu:{" "}
                        <span className="font-semibold">{menuId}</span>
                    </p>
                </div>

                <Link
                    to={`/restaurants/${restaurantId}`}
                    className="rounded-lg border border-slate-200 bg-white px-3 py-2 text-sm font-semibold text-slate-700 hover:bg-slate-50">
                    Back
                </Link>
            </div>

            <div className="rounded-xl border border-slate-200 bg-white p-5 shadow-sm">
                <div className="text-sm text-slate-700">TODO: Menu CRUD (categories + menu items + reorder).</div>
            </div>
        </div>
    );
};

export default MenuEditorPage;