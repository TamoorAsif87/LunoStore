import ReactQuill from "react-quill-new";
import "react-quill-new/dist/quill.snow.css"; // Import theme

const RichTextEditor = ({
  value,
  onChange,
}: {
  value: string;
  onChange: (val: string) => void;
}) => {
  return (
    <div className="form-control">
      <label className="label">
        <span className="label-text font-semibold">Description</span>
      </label>
      <ReactQuill
        theme="snow"
        value={value}
        onChange={onChange}
        style={{ height: "200px" }}
        className="bg-white"
        modules={{
          toolbar: [
            [{ header: [1, 2, 3, false] }],
            ["bold", "italic", "underline", "strike"],
            [{ list: "ordered" }, { list: "bullet" }],
            ["link", "image"],
            ["clean"],
          ],
        }}
      />
    </div>
  );
};

export default RichTextEditor;
