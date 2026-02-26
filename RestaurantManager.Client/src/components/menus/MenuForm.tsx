import { useState } from "react";
import type { MenuForm, MenuType } from "../../types/menu-types";
import { classNames } from "../helper";

type MenuFormProps = {
    initial: MenuForm
    onSubmit: (formData: MenuForm) => Promise<void>;
    onCancel: () => void;
};

const MenuForm = ({
    initial,
    onSubmit,
    onCancel
}: MenuFormProps) => {
    const [form, setForm] = useState<MenuForm>(initial);
    const [errors, setErrors] = useState<Record<string, string>>({ })

    const handleSubmit = async () => {

    };

    return (
        <form className="space-y-4" onSubmit={handleSubmit}>
            <div>
                <label className="text-xs font-medium text-slate-700">Name</label>
                <input
                    className={classNames(
                        "mt-1 w-full rounded-lg border px-3 py-2 text-sm outline-none focus:border-slate-400",
                        errors.name ? "border-rose-300" : "border-slate-200"
                    )}
                    value={form.name}
                    onChange={(e) => setForm((p) => ({ ...p, name: e.target.value }))}
                    placeholder="e.g. Bella Italia"
                />
                {errors.name && <div className="mt-1 text-xs text-rose-600">{errors.name}</div>}
            </div>

            <div>
                <label className="text-xs font-medium text-slate-700">Description</label>
                <textarea
                    className={classNames(
                        "mt-1 w-full rounded-lg border px-3 py-2 text-sm outline-none focus:border-slate-400",
                        errors.description ? "border-rose-300" : "border-slate-200"
                    )}
                    value={form.description}
                    rows={3}
                    onChange={(e) => setForm((p) => ({ ...p, description: e.target.value }))}
                    placeholder="e.g. Bella Italia"
                />
                {errors.description && <div className="mt-1 text-xs text-rose-600">{errors.description}</div>}
            </div>

            <div>
                <label className="text-xs font-medium text-slate-700">Img Url</label>
                <input
                    className={classNames(
                        "mt-1 w-full rounded-lg border px-3 py-2 text-sm outline-none focus:border-slate-400",
                        errors.location ? "border-rose-300" : "border-slate-200"
                    )}
                    value={form.imgUrl}
                    onChange={(e) => setForm((p) => ({ ...p, imgUrl: e.target.value }))}
                    placeholder="e.g. Sofia"
                />
                {errors.imgUrl && <div className="mt-1 text-xs text-rose-600">{errors.imgUrl}</div>}
            </div>

            <div>
                <label className="text-xs font-medium text-slate-700">Is Active</label>
                <input type="radio" />
                <select
                    className="mt-1 w-full rounded-lg border border-slate-200 px-3 py-2 text-sm outline-none focus:border-slate-400"
                    value={form.is}
                    onChange={(e) => setForm((p) => ({ ...p, type: e.target.value as MenuType }))}
                >
                    <option value="Default">Default</option>
                    <option value="Summer">Summer</option>
                    <option value="Winter">Winter</option>
                    <option value="Spring">Spring</option>
                    <option value="Autumn">Autumn</option>
                </select>
            </div>

            <div>
                <label className="text-xs font-medium text-slate-700">Menu type</label>
                <select
                    className="mt-1 w-full rounded-lg border border-slate-200 px-3 py-2 text-sm outline-none focus:border-slate-400"
                    value={form.type}
                    onChange={(e) => setForm((p) => ({ ...p, type: e.target.value as MenuType }))}
                >
                    <option value="Default">Default</option>
                    <option value="Summer">Summer</option>
                    <option value="Winter">Winter</option>
                    <option value="Spring">Spring</option>
                    <option value="Autumn">Autumn</option>
                </select>
            </div>

            <div>
                <label className="text-xs font-medium text-slate-700">Img Url</label>
                <input
                    className={classNames(
                        "mt-1 w-full rounded-lg border px-3 py-2 text-sm outline-none focus:border-slate-400",
                        errors.cuisine ? "border-rose-300" : "border-slate-200"
                    )}
                    value={form.imgUrl}
                    onChange={(e) => setForm((p) => ({ ...p, imgUrl: e.target.value }))}
                    placeholder="e.g. http://localhost:7123/manage-restaurants"
                />
                {errors.cuisine && <div className="mt-1 text-xs text-rose-600">{errors.imgUrl}</div>}
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
};

export default MenuForm;