import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useGetProfile } from "../features/auth/hooks/useGetProfile";
import { useUpdateProfile } from "../features/auth/hooks/useUpdateProfile";
import {
  updateProfileSchema,
  type UpdateProfileSchema,
} from "../features/auth/schemas/updateProfileSchema";

import FormInput from "../components/FormInput";
import { useEffect, useState } from "react";
import { FiCamera } from "react-icons/fi";
import Loading from "../components/Loading";
import { useTitle } from "../hooks/useTitle";

const UserProfile = () => {
  const { data: profile, isLoading } = useGetProfile();
  const { mutate: updateProfile, isPending } = useUpdateProfile();
  useTitle(`${profile?.name} - Profile | LunoStore`);
  const methods = useForm<UpdateProfileSchema>({
    resolver: zodResolver(updateProfileSchema),
  });

  const [imagePreview, setImagePreview] = useState<string | null>(null);

  // Pre-fill form once profile data is loaded
  useEffect(() => {
    if (profile) {
      methods.reset({
        name: profile.name,
        address: profile.address || "",
        phone: profile.phone || "",
        city: profile.city || "",
        country: profile.country || "",
        profileImageFile: undefined, // handled separately
      });
      setImagePreview(profile.profileImage || null);
    }
  }, [profile, methods]);

  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      methods.setValue("profileImageFile", file);
      const reader = new FileReader();
      reader.onloadend = () => {
        setImagePreview(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  };

  const onSubmit = (data: UpdateProfileSchema) => {
    const formData = new FormData();
    formData.append("Name", data.name);
    formData.append("Address", data.address || "");
    formData.append("Phone", data.phone || "");
    formData.append("City", data.city || "");
    formData.append("Country", data.country || "");
    if (data.profileImageFile) {
      formData.append("ProfileImageFile", data.profileImageFile);
    }

    updateProfile(formData);
  };

  if (isLoading) return <Loading />;

  return (
    <div className="max-w-xl mx-auto mt-8 p-6 shadow rounded bg-white">
      <h2 className="text-2xl font-bold mb-4 text-center">Update Profile</h2>

      <div className="flex justify-center mb-6">
        <div className="relative w-32 h-32">
          <img
            src={imagePreview || "/default.png"}
            alt="Profile"
            className="rounded-full w-32 h-32 object-cover border"
          />
          <label className="absolute bottom-0 right-0 bg-primary text-white p-2 rounded-full cursor-pointer">
            <FiCamera />
            <input
              type="file"
              accept="image/*"
              className="hidden"
              onChange={handleImageChange}
            />
          </label>
        </div>
      </div>

      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <FormInput name="name" label="Name" />
          <FormInput
            name="email"
            label="Email"
            defaultValue={profile?.email}
            readOnly
          />
          <FormInput
            name="userName"
            label="Username"
            defaultValue={profile?.userName}
            readOnly
          />
          <FormInput name="address" label="Address" />
          <FormInput name="phone" label="Phone" />
          <FormInput name="city" label="City" />
          <FormInput name="country" label="Country" />

          <button
            type="submit"
            disabled={isPending}
            className="btn btn-primary w-full mt-4"
          >
            {isPending ? "Updating..." : "Update Profile"}
          </button>
        </form>
      </FormProvider>
    </div>
  );
};

export default UserProfile;
