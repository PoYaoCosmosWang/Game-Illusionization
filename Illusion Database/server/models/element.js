const mongoose = require('mongoose');

const elementSchema = new mongoose.Schema({
  name: String,
  iconURL: String,
  level: Number,
  subelements: [mongoose.ObjectId],
});

module.exports = {
  model: mongoose.model('Element', elementSchema),
  schema: elementSchema,
};
